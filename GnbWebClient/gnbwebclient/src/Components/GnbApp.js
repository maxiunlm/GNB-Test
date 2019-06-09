import React, { Component } from 'react';
import SkuClient from '../Clients/SkuClient';
import RatesClient from '../Clients/RatesClient';
import TransactionsClient from '../Clients/TransactionsClient';
import { RatesContext } from '../Contexts/RatesContext';
import { TransactionsContext } from '../Contexts/TransactionsContext';
import { SkuSummaryContext } from '../Contexts/SkuSummaryContext';
import Rates from './Rates';
import Transactions from './Transactions';
import SkuSummary from './SkuSummary';

const stringEmty = '';
const visible = '';
const invisible = 'hidden';

export default class GnbApp extends Component {
    constructor(props) {
        super(props);

        this.skuClient = new SkuClient();
        this.ratesClient = new RatesClient();
        this.transactionsClient = new TransactionsClient();
        this.id = 0;
        this.state = {
            skuOptions: [],
            rates: [],
            transactions: [],
            selectedSku: stringEmty,
            totlSku: 0,
            skuSummaryVisibility: visible,
            transactionsVisibility: invisible,
            ratesVisibility: invisible,
            spinnerVisibility: invisible
        }

        this.onSkuChange = this.onSkuChange.bind(this);
        this.onShowRatesClick = this.onShowRatesClick.bind(this);
        this.onShowSkuSummaryClick = this.onShowSkuSummaryClick.bind(this);
        this.onShowTransactionsClick = this.onShowTransactionsClick.bind(this);
        this.getRatesContextData = this.getRatesContextData.bind(this);
        this.getSkuOptions = this.getSkuOptions.bind(this);
        this.getId = this.getId.bind(this);
    }

    async componentDidMount() {
        this.setState({ spinnerVisibility: visible });

        let rates = await this.ratesClient.listRates();
        let transactions = await this.transactionsClient.listTransactions();
        let skuOptions = await this.skuClient.listSkus();

        this.setState({
            skuOptions: skuOptions,
            transactions: transactions,
            rates: rates,
            spinnerVisibility: invisible
        });
    }

    async onSkuChange(option) {
        this.setState({ spinnerVisibility: visible });

        let skuInfo = await this.skuClient.getSku(option.value)

        this.setState({
            selectedSku: option.value,
            totlSku: skuInfo.total,
            transactionsSku: skuInfo.transactions,
            spinnerVisibility: invisible
        });
    }

    onShowSkuSummaryClick(event) {
        this.setState({
            skuSummaryVisibility: visible,
            transactionsVisibility: invisible,
            ratesVisibility: invisible
        });
    }

    onShowTransactionsClick() {
        this.setState({
            skuSummaryVisibility: invisible,
            transactionsVisibility: visible,
            ratesVisibility: invisible
        });
    }

    onShowRatesClick(event) {
        this.setState({
            skuSummaryVisibility: invisible,
            transactionsVisibility: invisible,
            ratesVisibility: visible
        });
    }

    getSkuOptions() {
        let options = this.state.skuOptions.map(option => ({
            value: option,
            label: option,
        }));

        return options
    }

    getId() {
        return ++this.id;
    }

    getRatesContextData() {
        let ratesContextData = {
            ratesVisibility: this.state.ratesVisibility,
            spinnerVisibility: this.state.spinnerVisibility,
            rates: this.state.rates
        };

        return ratesContextData;
    }

    getTransactionsContextData() {
        let transactionsContextData = {
            transactionsVisibility: this.state.transactionsVisibility,
            spinnerVisibility: this.state.spinnerVisibility,
            transactions: this.state.transactions
        };

        return transactionsContextData;
    }

    getSkuSummaryContextData() {
        let options = this.getSkuOptions();
        let transactionsSku = this.state.transactionsSku || [];

        let skuSummaryContextData = {
            skuSummaryVisibility: this.state.skuSummaryVisibility,
            spinnerVisibility: this.state.spinnerVisibility,
            loadSkuDisnabled: this.state.loadSkuDisnabled,
            options: options,
            transactions: transactionsSku
        };

        return skuSummaryContextData;
    }

    render() {
        let skuSummaryContextData = this.getSkuSummaryContextData();
        let ratesContextData = this.getRatesContextData();
        let transactionsContextData = this.getTransactionsContextData();

        return (
            <div className="container">
                <SkuSummaryContext.Provider value={skuSummaryContextData}>
                    <SkuSummary
                        onSkuChange={this.onSkuChange}
                        onShowRatesClick={this.onShowRatesClick}
                        onShowTransactionsClick={this.onShowTransactionsClick}
                    />
                </SkuSummaryContext.Provider>
                <TransactionsContext.Provider value={transactionsContextData}>
                    <Transactions
                        onShowSkuSummaryClick={this.onShowSkuSummaryClick}
                        onShowRatesClick={this.onShowRatesClick}
                    />
                </TransactionsContext.Provider>
                <RatesContext.Provider value={ratesContextData}>
                    <Rates
                        onShowSkuSummaryClick={this.onShowSkuSummaryClick}
                        onShowTransactionsClick={this.onShowTransactionsClick}
                    />
                </RatesContext.Provider>
            </div>
        );
    }
}
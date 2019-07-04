import React, { Component } from 'react';
import LoginClient from '../Clients/LoginClient';
import SkuClient from '../Clients/SkuClient';
import RatesClient from '../Clients/RatesClient';
import TransactionsClient from '../Clients/TransactionsClient';
import { RatesContext } from '../Contexts/RatesContext';
import { TransactionsContext } from '../Contexts/TransactionsContext';
import { SkuSummaryContext } from '../Contexts/SkuSummaryContext';
import Rates from './Rates';
import Transactions from './Transactions';
import SkuSummary from './SkuSummary';
import Login from './Login';

const emstyString = '';
const visible = '';
const invisible = 'hidden';

export default class GnbApp extends Component {
    constructor(props) {
        super(props);

        this.loginClient = new LoginClient();
        this.skuClient = new SkuClient(this.loginClient);
        this.ratesClient = new RatesClient(this.loginClient);
        this.transactionsClient = new TransactionsClient(this.loginClient);
        this.id = 0;
        this.state = {
            skuOptions: [],
            rates: [],
            transactions: [],
            selectedSku: emstyString,
            totlSku: 0,
            skuSummaryVisibility: visible,
            transactionsVisibility: invisible,
            ratesVisibility: invisible,
            spinnerVisibility: invisible
        }

        this.doLogin = this.doLogin.bind(this);
        this.loadData = this.loadData.bind(this);
        this.hideSpinner = this.hideSpinner.bind(this);
        this.onSkuChange = this.onSkuChange.bind(this);
        this.onShowRatesClick = this.onShowRatesClick.bind(this);
        this.onShowSkuSummaryClick = this.onShowSkuSummaryClick.bind(this);
        this.onShowTransactionsClick = this.onShowTransactionsClick.bind(this);
        this.getRatesContextData = this.getRatesContextData.bind(this);
        this.getSkuOptions = this.getSkuOptions.bind(this);
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

    async doLogin(username, password) {
        this.setState({ spinnerVisibility: visible });
        let user = await this.loginClient.login(username, password);
        this.loadData().then(this.hideSpinner);
        return user;
    }

    async loadData() {
        let rates = await this.ratesClient.listRates();
        let transactions = await this.transactionsClient.listTransactions();
        let skuOptions = await this.skuClient.listSkus();

        this.setState({
            skuOptions: skuOptions,
            transactions: transactions,
            rates: rates
        });
    }

    hideSpinner() {
        this.setState({ spinnerVisibility: invisible });
    }

    getSkuOptions() {
        let options = this.state.skuOptions.map(option => ({
            value: option,
            label: option,
        }));

        return options
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
            selectedSku: this.state.selectedSku,
            totlSku: this.state.totlSku,
            skuSummaryVisibility: this.state.skuSummaryVisibility,
            spinnerVisibility: this.state.spinnerVisibility,
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
                <Login doLogin={this.doLogin} />
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
import 'bootstrap/dist/css/bootstrap.css';
import '../Styles/sku.css';
import React, { Component } from 'react';
import Select from 'react-select';
import SkuClient from '../Clients/SkuClient';
import RatesClient from '../Clients/RatesClient';
import TransactionsClient from '../Clients/TransactionsClient';

const stringEmty = '';
const visible = '';
const invisible = 'hidden';

export default class Sku extends Component {
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

    render() {
        let options = this.getSkuOptions();
        let transactionsSku = this.state.transactionsSku || [];

        return (
            <div className="container">
                <div
                    id="skuSummary"
                    className={this.state.skuSummaryVisibility}
                >
                    <h4 className="alert alert-primary" role="alert">
                        Resumen Sku
                        <div className={this.state.spinnerVisibility + ' float-right'}>
                            <span className="spinner-grow"></span>
                        </div>
                    </h4>
                    <div>
                        <div className="btn-group float-right" role="group" aria-label="Menu">
                            <button
                                className="btn btn-secondary skuMenuButton"
                                onClick={this.onShowTransactionsClick}
                            >&lt; Transacciones</button>
                            <button
                                disabled={this.state.loadSkuDisnabled}
                                className="btn btn-secondary skuMenuButton"
                                onClick={this.onShowRatesClick}
                            >Conversiones (Rates) &gt;</button>
                        </div>
                    </div>
                    <div className="clearfix" />
                    <br />
                    <div className="float-left">
                        <label>SKU</label>
                        <Select
                            id="skuSelect"
                            className="skuSelect"
                            onChange={this.onSkuChange}
                            options={options}
                            placeholder="Search a SKU"
                        />
                        &nbsp;
                    </div>
                    <div className="float-left">
                        &nbsp;
                        <div className="list-group skuTransactions">
                            <div className="list-group-item list-group-item-primary">
                                Transactions para {this.state.selectedSku} en &euro;:
                            </div>
                            <div className="skuTransactionItems">
                                {transactionsSku.map(transaction => (
                                    <div key={this.getId()} className="list-group-item text-right">{transaction.amount}</div>
                                ))}
                            </div>
                            <div className="list-group-item list-group-item-primary">
                                Total in &euro;: {this.state.totlSku}
                            </div>
                        </div>
                    </div>
                </div>
                <div
                    id="transactions"
                    className={this.state.transactionsVisibility}
                >
                    <h4 className="alert alert-secondary" role="alert">
                        Transacciones
                        <div className={this.state.spinnerVisibility + ' float-right'}>
                            <span className="spinner-grow"></span>
                        </div>
                    </h4>
                    <div>
                        <div className="btn-group float-right" role="group" aria-label="Menu">
                            <button
                                disabled={this.state.loadSkuDisnabled}
                                className="btn btn-secondary skuMenuButton"
                                onClick={this.onShowRatesClick}
                            >&lt; Conversiones (Rates)</button>
                            <button
                                className="btn btn-primary skuMenuButton"
                                onClick={this.onShowSkuSummaryClick}
                            >Resumen Sku &gt;</button>
                        </div>
                    </div>
                    <div className="clearfix" />
                    <br />
                    <div>
                        <table className="table table-dark">
                            <thead>
                                <tr>
                                    <th scope="col">Sku</th>
                                    <th scope="col">Currency</th>
                                    <th scope="col">amount</th>
                                </tr>
                            </thead>
                            <tbody className="scroll">
                                {this.state.transactions.map(transaction => (
                                    <tr key={this.getId()}>
                                        <td>{transaction.sku}</td>
                                        <td>{transaction.currency}</td>
                                        <td>{transaction.amount}</td>
                                    </tr>
                                ))}
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th scope="col">Sku</th>
                                    <th scope="col">Currency</th>
                                    <th scope="col">amount</th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
                <div
                    id="rates"
                    className={this.state.ratesVisibility}
                >
                    <h4 className="alert alert-secondary" role="alert">
                        Conversiones (Rates)
                        <div className={this.state.spinnerVisibility + ' float-right'}>
                            <span className="spinner-grow"></span>
                        </div>
                    </h4>
                    <div>
                        <div className="btn-group float-right" role="group" aria-label="Menu">
                            <button
                                className="btn btn-primary skuMenuButton"
                                onClick={this.onShowSkuSummaryClick}
                            >&lt; Resumen Sku</button>
                            <button
                                className="btn btn-secondary skuMenuButton"
                                onClick={this.onShowTransactionsClick}
                            >Transacciones &gt;</button>
                        </div>
                    </div>
                    <div className="clearfix" />
                    <br />
                    <div>
                        <table className="table table-dark">
                            <thead>
                                <tr>
                                    <th scope="col">From</th>
                                    <th scope="col">To</th>
                                    <th scope="col">Rate</th>
                                </tr>
                            </thead>
                            <tbody className="scroll">
                                {this.state.rates.map(rate => (
                                    <tr key={this.getId()}>
                                        <td>{rate.from}</td>
                                        <td>{rate.to}</td>
                                        <td>{rate.rate}</td>
                                    </tr>
                                ))}
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th scope="col">From</th>
                                    <th scope="col">To</th>
                                    <th scope="col">Rate</th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        );
    }
}
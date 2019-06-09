import 'bootstrap/dist/css/bootstrap.css';
import '../Styles/sku.css';
import React, { Component } from 'react';
import Select from 'react-select';
import { SkuSummaryContext } from '../Contexts/SkuSummaryContext';

export default class SkuSummary extends Component {
    constructor(props) {
        super(props)
        this.id = 0;

        this.getId = this.getId.bind(this);
    }

    getId() {
        return ++this.id;
    }

    render() {
        return (
            <div>
                <SkuSummaryContext.Consumer>
                    {(context) => (<div
                        id="skuSummary"
                        className={context.skuSummaryVisibility}
                    >
                        <h4 className="alert alert-primary" role="alert">
                            Resumen Sku
                        <div className={context.spinnerVisibility + ' float-right'}>
                                <span className="spinner-grow"></span>
                            </div>
                        </h4>
                        <div>
                            <div className="btn-group float-right" role="group" aria-label="Menu">
                                <button
                                    className="btn btn-secondary skuMenuButton"
                                    onClick={this.props.onShowTransactionsClick}
                                >&lt; Transacciones</button>
                                <button
                                    disabled={context.loadSkuDisnabled}
                                    className="btn btn-secondary skuMenuButton"
                                    onClick={this.props.onShowRatesClick}
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
                                onChange={this.props.onSkuChange}
                                options={context.options}
                                placeholder="Search a SKU"
                            />
                        </div>
                        <div className="float-left">
                            <div className="list-group skuTransactions">
                                <div className="list-group-item list-group-item-primary">
                                    Transactions para {context.selectedSku} en &euro;:
                            </div>
                                <div className="skuTransactionItems">
                                    {context.transactions.map(transaction => (
                                        <div key={this.getId()} className="list-group-item text-right">{transaction.amount}</div>
                                    ))}
                                </div>
                                <div className="list-group-item list-group-item-primary">
                                    Total in &euro;: {context.totlSku}
                                </div>
                            </div>
                        </div>
                    </div>
                    )}
                </SkuSummaryContext.Consumer>
            </div>
        );
    }
}

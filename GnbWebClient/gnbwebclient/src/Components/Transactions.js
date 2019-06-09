import 'bootstrap/dist/css/bootstrap.css';
import '../Styles/sku.css';
import React, { Component } from 'react';
import { TransactionsContext } from '../Contexts/TransactionsContext';

export default class Transactions extends Component {
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
                <TransactionsContext.Consumer>
                    {(context) => (
                        <div
                            id="transactions"
                            className={context.transactionsVisibility}
                        >
                            <h4 className="alert alert-secondary" role="alert">
                                Transacciones
                        <div className={context.spinnerVisibility + ' float-right'}>
                                    <span className="spinner-grow"></span>
                                </div>
                            </h4>
                            <div>
                                <div className="btn-group float-right" role="group" aria-label="Menu">
                                    <button
                                        disabled={context.loadSkuDisnabled}
                                        className="btn btn-secondary skuMenuButton"
                                        onClick={this.props.onShowRatesClick}
                                    >&lt; Conversiones (Rates)</button>
                                    <button
                                        className="btn btn-primary skuMenuButton"
                                        onClick={this.props.onShowSkuSummaryClick}
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
                                        {context.transactions.map(transaction => (
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
                    )}
                </TransactionsContext.Consumer>
            </div>
        );
    }
}
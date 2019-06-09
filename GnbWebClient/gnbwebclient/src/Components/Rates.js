import 'bootstrap/dist/css/bootstrap.css';
import '../Styles/sku.css';
import React, { Component } from 'react';
import { RatesContext } from '../Contexts/RatesContext';

export default class Rates extends Component {
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
                <RatesContext.Consumer>
                    {(context) => (
                        <div
                            id="rates"
                            className={context.ratesVisibility}
                        >
                            <h4 className="alert alert-secondary" role="alert">
                                Conversiones (Rates)
                                <div className={context.spinnerVisibility + ' float-right'}>
                                    <span className="spinner-grow"></span>
                                </div>
                            </h4><div>
                                <div className="btn-group float-right" role="group" aria-label="Menu">
                                    <button
                                        className="btn btn-primary skuMenuButton"
                                        onClick={this.props.onShowSkuSummaryClick}
                                    >&lt; Resumen Sku</button>
                                    <button
                                        className="btn btn-secondary skuMenuButton"
                                        onClick={this.props.onShowTransactionsClick}
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
                                        {context.rates.map(rate => (
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
                    )}
                </RatesContext.Consumer>
            </div>
        );
    }
}

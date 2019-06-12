import 'bootstrap/dist/css/bootstrap.css';
import '../Styles/sku.css';
import React, { Component } from 'react';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
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

    trClassFormat(row, rowIndex) {
        // row is the current row data
        return rowIndex % 2 === 0 ? "tr-odd" : "tr-even";  // return class name.
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
                                <BootstrapTable data={context.transactions} pagination trClassName={this.trClassFormat} exportCSV
                                    csvFileName="transactions-export.csv">
                                    <TableHeaderColumn dataField="sku" isKey={true} dataSort={true}>Sku</TableHeaderColumn>
                                    <TableHeaderColumn dataField="currency" dataSort={true}>Currency</TableHeaderColumn>
                                    <TableHeaderColumn dataField="amount" headerAlign="left" dataAlign="right">Amount</TableHeaderColumn>
                                </BootstrapTable>
                            </div>
                        </div>
                    )}
                </TransactionsContext.Consumer>
            </div>
        );
    }
}
import 'bootstrap/dist/css/bootstrap.css';
import '../Styles/sku.css';
import React, { Component } from 'react';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { RatesContext } from '../Contexts/RatesContext';

export default class Rates extends Component {
    constructor(props) {
        super(props)
        this.id = 0;

        this.trClassFormat = this.trClassFormat.bind(this);
    }

    trClassFormat(row, rowIndex) {
        // row is the current row data
        return rowIndex % 2 === 0 ? 'tr-even' : 'tr-odd';  // return class name.
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
                                <BootstrapTable data={context.rates} pagination trClassName={this.trClassFormat} exportCSV
                                    csvFileName="rates-export.csv">
                                    <TableHeaderColumn dataField="from" isKey={true} dataSort={true}>From</TableHeaderColumn>
                                    <TableHeaderColumn dataField="to" dataSort={true}>To</TableHeaderColumn>
                                    <TableHeaderColumn dataField="rate" headerAlign="left" dataAlign="right">Rate</TableHeaderColumn>
                                </BootstrapTable>
                            </div>
                        </div>
                    )}
                </RatesContext.Consumer>
            </div>
        );
    }
}

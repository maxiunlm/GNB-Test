import 'bootstrap/dist/css/bootstrap.css';
import '../Styles/sku.css';
import React, { Component } from 'react';
import Select from 'react-select';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import { SkuSummaryContext } from '../Contexts/SkuSummaryContext';

export default class SkuSummary extends Component {
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
                        <div className="float-left skuTransactions">
                            <BootstrapTable data={context.transactions} pagination trClassName={this.trClassFormat}>
                                <TableHeaderColumn dataField="amount" isKey={true} dataSort={true} headerAlign="left" dataAlign="right">Transactions para {context.selectedSku} en &euro;</TableHeaderColumn>
                            </BootstrapTable>
                            <div className="list-group">
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

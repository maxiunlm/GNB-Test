import React from 'react';
import ReactDOM from 'react-dom';
import Transactions from '../../Components/Transactions';
import { TransactionsContext } from '../../Contexts/TransactionsContext';
import { CommonFakes } from '../Fakes/CommonFakes'

describe('Transactions', () => {
    describe('Component', () => {
        it('renders without crashing', () => {
            let transactionsContextData = {
                transactionsVisibility: CommonFakes.emptyString,
                spinnerVisibility: CommonFakes.emptyString,
                transactions: CommonFakes.emptyArray
            };

            const div = document.createElement('div');
            ReactDOM.render(
                <TransactionsContext.Provider value={transactionsContextData}>
                    <Transactions
                        onShowSkuSummaryClick={() => { }}
                        onShowRatesClick={() => { }}
                    />
                </TransactionsContext.Provider>
                , div);
            ReactDOM.unmountComponentAtNode(div);
        });
    });

    describe('constructor', () => {
        it('Instancing this Object, then the Object is initilaized', () => {
            let sut = new Transactions();

            expect(sut.id).toEqual(CommonFakes.zero);
        });
    });

    describe('getId', () => {
        it('Invoking this Method, then increase the "id" in 1', () => {
            let sut = new Transactions();
            let oldId = sut.id;

            let result = sut.getId();

            expect(oldId).toEqual(CommonFakes.zero);
            expect(result).toEqual(CommonFakes.one);
            expect(result).toEqual(sut.id);
        });
    });
});
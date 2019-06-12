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

    describe('trClassFormat', () => {
        it('with some row and an "odd" index row, returns "tr-odd" css class name', () => {
            let sut = new Transactions();
            let row = CommonFakes.zero;
            let oddRowIndes = CommonFakes.one;

            let result = sut.trClassFormat(row, oddRowIndes);

            expect(result).toEqual(CommonFakes.trOdd);
        });

        it('with some row and an "even" index row, returns "tr-even" css class name', () => {
            let sut = new Transactions();
            let row = CommonFakes.zero;
            let evenRowIndes = CommonFakes.two;

            let result = sut.trClassFormat(row, evenRowIndes);

            expect(result).toEqual(CommonFakes.trEven);
        });
    });
});
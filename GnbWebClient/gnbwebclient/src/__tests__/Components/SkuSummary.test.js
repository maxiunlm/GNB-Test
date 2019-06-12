import React from 'react';
import ReactDOM from 'react-dom';
import SkuSummary from '../../Components/SkuSummary';
import { SkuSummaryContext } from '../../Contexts/SkuSummaryContext';
import { CommonFakes } from '../Fakes/CommonFakes'

describe('SkuSummary', () => {
    describe('Component', () => {
        it('renders without crashing', () => {
            let skuSummaryContextData = {
                skuSummaryVisibility: CommonFakes.emptyString,
                spinnerVisibility: CommonFakes.emptyString,
                transactions: CommonFakes.emptyArray
            };

            const div = document.createElement('div');
            ReactDOM.render(
                <SkuSummaryContext.Provider value={skuSummaryContextData}>
                    <SkuSummary
                        onSkuChange={() => { }}
                        onShowRatesClick={() => { }}
                        onShowTransactionsClick={() => { }}
                    />
                </SkuSummaryContext.Provider>
                , div);
            ReactDOM.unmountComponentAtNode(div);
        });
    });

    describe('constructor', () => {
        it('Instancing this Object, then the Object is initilaized', () => {
            let sut = new SkuSummary();

            expect(sut.id).toEqual(CommonFakes.zero);
        });
    });

    describe('trClassFormat', () => {
        it('with some row and an "odd" index row, returns "tr-odd" css class name', () => {
            let sut = new SkuSummary();
            let row = CommonFakes.zero;
            let oddRowIndes = CommonFakes.one;

            let result = sut.trClassFormat(row, oddRowIndes);

            expect(result).toEqual(CommonFakes.trOdd);
        });

        it('with some row and an "even" index row, returns "tr-even" css class name', () => {
            let sut = new SkuSummary();
            let row = CommonFakes.zero;
            let evenRowIndes = CommonFakes.two;

            let result = sut.trClassFormat(row, evenRowIndes);

            expect(result).toEqual(CommonFakes.trEven);
        });
    });
});
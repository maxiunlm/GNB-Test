import React from 'react';
import ReactDOM from 'react-dom';
import Rates from '../../Components/Rates';
import { RatesContext } from '../../Contexts/RatesContext';
import { CommonFakes } from '../Fakes/CommonFakes'

describe('Rates', () => {
    describe('Component', () => {
        it('renders without crashing', () => {
            let ratesContextData = {
                ratesVisibility: CommonFakes.emptyString,
                spinnerVisibility: CommonFakes.emptyString,
                rates: CommonFakes.emptyArray
            };

            const div = document.createElement('div');
            ReactDOM.render(
                <RatesContext.Provider value={ratesContextData}>
                    <Rates
                        onShowSkuSummaryClick={() => { }}
                        onShowTransactionsClick={() => { }}
                    />
                </RatesContext.Provider>
                , div);
            ReactDOM.unmountComponentAtNode(div);
        });
    });

    describe('constructor', () => {
        it('Instancing this Object, then the Object is initilaized', () => {
            let sut = new Rates();

            expect(sut.id).toEqual(CommonFakes.zero);
        });
    });

    describe('trClassFormat', () => {
        it('with some row and an "odd" index row, returns "tr-odd" css class name', () => {
            let sut = new Rates();
            let row = CommonFakes.zero;
            let oddRowIndes = CommonFakes.one;

            let result = sut.trClassFormat(row, oddRowIndes);

            expect(result).toEqual(CommonFakes.trOdd);
        });

        it('with some row and an "even" index row, returns "tr-even" css class name', () => {
            let sut = new Rates();
            let row = CommonFakes.zero;
            let evenRowIndes = CommonFakes.two;

            let result = sut.trClassFormat(row, evenRowIndes);

            expect(result).toEqual(CommonFakes.trEven);
        });
    });
});
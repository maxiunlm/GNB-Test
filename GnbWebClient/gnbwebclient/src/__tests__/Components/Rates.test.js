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

    describe('getId', () => {
        it('Invoking this Method, then increase the "id" in 1', () => {
            let sut = new Rates();
            let oldId = sut.id;

            let result = sut.getId();

            expect(oldId).toEqual(CommonFakes.zero);
            expect(result).toEqual(CommonFakes.one);
            expect(result).toEqual(sut.id);
        });
    });
});
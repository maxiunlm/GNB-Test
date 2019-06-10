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

    describe('getId', () => {
        it('Invoking this Method, then increase the "id" in 1', () => {
            let sut = new SkuSummary();
            let oldId = sut.id;

            let result = sut.getId();

            expect(oldId).toEqual(CommonFakes.zero);
            expect(result).toEqual(CommonFakes.one);
            expect(result).toEqual(sut.id);
        });
    });
});
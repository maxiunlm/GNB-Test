/* global expect, spyOn */
import React from 'react';
import ReactDOM from 'react-dom';
import SkuClient from '../../Clients/SkuClient';
import RatesClient from '../../Clients/RatesClient';
import TransactionsClient from '../../Clients/TransactionsClient';
import GnbApp from '../../Components/GnbApp';
import { CommonFakes } from '../Fakes/CommonFakes'

describe('GnbApp', () => {
    describe('Component', () => {
        it('renders without crashing', () => {
            const div = document.createElement('div');
            ReactDOM.render(<GnbApp />, div);
            ReactDOM.unmountComponentAtNode(div);
        });
    });

    describe('constructor', () => {
        it('Instancing this Object, then the Object is initilaized', () => {
            let sut = new GnbApp();

            expect(sut.skuClient instanceof SkuClient).toBeTruthy();
            expect(sut.ratesClient instanceof RatesClient).toBeTruthy();
            expect(sut.transactionsClient instanceof TransactionsClient).toBeTruthy();
            expect(sut.id).toEqual(CommonFakes.zero);
            expect(sut.state.skuOptions instanceof Array).toBeTruthy();
            expect(sut.state.rates instanceof Array).toBeTruthy();
            expect(sut.state.transactions instanceof Array).toBeTruthy();
            expect(sut.state.selectedSku).toEqual(CommonFakes.emptyString);
            expect(sut.state.totlSku).toEqual(CommonFakes.zero);
            expect(sut.state.skuSummaryVisibility).toEqual(CommonFakes.visible);
            expect(sut.state.transactionsVisibility).toEqual(CommonFakes.invisible);
            expect(sut.state.ratesVisibility).toEqual(CommonFakes.invisible);
            expect(sut.state.spinnerVisibility).toEqual(CommonFakes.invisible);
        });
    });

    describe('loadData', () => {
        it('Invokes "setState" method once', async () => {
            spyOn(GnbApp.prototype, 'setState').and.callFake(() => { });
            spyOn(RatesClient.prototype, 'listRates').and.callFake(async () => { });
            spyOn(TransactionsClient.prototype, 'listTransactions').and.callFake(async () => { });
            spyOn(SkuClient.prototype, 'listSkus').and.callFake(async () => { });
            let sut = new GnbApp();

            await sut.loadData();

            expect(GnbApp.prototype.setState.calls.count()).toEqual(CommonFakes.once);
        });

        it('Invokes "listRates" method from "RatesClient" object once', async () => {
            spyOn(GnbApp.prototype, 'setState').and.callFake(() => { });
            spyOn(RatesClient.prototype, 'listRates').and.callFake(async () => { });
            spyOn(TransactionsClient.prototype, 'listTransactions').and.callFake(async () => { });
            spyOn(SkuClient.prototype, 'listSkus').and.callFake(async () => { });
            let sut = new GnbApp();

            await sut.loadData();

            expect(RatesClient.prototype.listRates.calls.count()).toEqual(CommonFakes.once);
        });

        it('Invokes "listTransactions" method from "TransactionsClient" object once', async () => {
            spyOn(GnbApp.prototype, 'setState').and.callFake(() => { });
            spyOn(RatesClient.prototype, 'listRates').and.callFake(async () => { });
            spyOn(TransactionsClient.prototype, 'listTransactions').and.callFake(async () => { });
            spyOn(SkuClient.prototype, 'listSkus').and.callFake(async () => { });
            let sut = new GnbApp();

            await sut.loadData();

            expect(TransactionsClient.prototype.listTransactions.calls.count()).toEqual(CommonFakes.once);
        });

        it('Invokes "listSkus" method from "SkuClient" object once', async () => {
            spyOn(GnbApp.prototype, 'setState').and.callFake(() => { });
            spyOn(RatesClient.prototype, 'listRates').and.callFake(async () => { });
            spyOn(TransactionsClient.prototype, 'listTransactions').and.callFake(async () => { });
            spyOn(SkuClient.prototype, 'listSkus').and.callFake(async () => { });
            let sut = new GnbApp();

            await sut.loadData();

            expect(SkuClient.prototype.listSkus.calls.count()).toEqual(CommonFakes.once);
        });
    });

    describe('onSkuChange', () => {
        it('Invokes "setState" method twice', async () => {
            spyOn(GnbApp.prototype, 'setState').and.callFake(() => { });
            spyOn(SkuClient.prototype, 'getSku').and.callFake(async () => {
                return Promise.resolve(CommonFakes.emptySkuInfo);
            });
            let sut = new GnbApp();

            await sut.onSkuChange(CommonFakes.skuOption);

            expect(GnbApp.prototype.setState.calls.count()).toEqual(CommonFakes.twice);
        });

        it('Invokes "getSku" method from "SkuClient" object once', async () => {
            spyOn(GnbApp.prototype, 'setState').and.callFake(() => { });
            spyOn(SkuClient.prototype, 'getSku').and.callFake(async () => {
                return Promise.resolve(CommonFakes.emptySkuInfo);
            });
            let sut = new GnbApp();

            await sut.onSkuChange(CommonFakes.skuOption);

            expect(SkuClient.prototype.getSku).toHaveBeenCalledWith(CommonFakes.skuOption.value);
            expect(SkuClient.prototype.getSku.calls.count()).toEqual(CommonFakes.once);
        });
    });

    describe('onShowSkuSummaryClick', () => {
        it('Invokes "setState" method once', () => {
            spyOn(GnbApp.prototype, 'setState').and.callFake(() => { });
            let sut = new GnbApp();

            sut.onShowSkuSummaryClick(CommonFakes.emptyEvent);

            expect(GnbApp.prototype.setState.calls.count()).toEqual(CommonFakes.once);
        });
    });

    describe('onShowRatesClick', () => {
        it('Invokes "setState" method once', () => {
            spyOn(GnbApp.prototype, 'setState').and.callFake(() => { });
            let sut = new GnbApp();

            sut.onShowRatesClick(CommonFakes.emptyEvent);

            expect(GnbApp.prototype.setState.calls.count()).toEqual(CommonFakes.once);
        });
    });

    describe('onShowTransactionsClick', () => {
        it('Invokes "setState" method once', () => {
            spyOn(GnbApp.prototype, 'setState').and.callFake(() => { });
            let sut = new GnbApp();

            sut.onShowTransactionsClick();

            expect(GnbApp.prototype.setState.calls.count()).toEqual(CommonFakes.once);
        });
    });

    describe('getSkuOptions', () => {
        it('Returns a mapped Array "[{value: option, label: option}, ...]" From an Array of Strings with no Options', () => {
            let sut = new GnbApp();
            sut.state.skuOptions = CommonFakes.emptyArray;

            let result = sut.getSkuOptions();

            expect(result.length).toEqual(CommonFakes.emptyArray.length);
            expect(result.length).toEqual(CommonFakes.zero);
        });

        it('Returns a mapped Array "[{value: option, label: option}, ...]" From an Array of Strings with one Option', () => {
            let sut = new GnbApp();
            sut.state.skuOptions = CommonFakes.oneOption;

            let result = sut.getSkuOptions();

            expect(result[CommonFakes.firstIndex].value).toEqual(CommonFakes.oneOption[CommonFakes.firstIndex]);
            expect(result[CommonFakes.firstIndex].value).toEqual(result[CommonFakes.firstIndex].label);
        });

        it('Returns a mapped Array "[{value: option, label: option}, ...]" From an Array of Strings with two Options', () => {
            let sut = new GnbApp();
            sut.state.skuOptions = CommonFakes.twoOptions;

            let result = sut.getSkuOptions();

            expect(result[CommonFakes.firstIndex].value).toEqual(CommonFakes.twoOptions[CommonFakes.firstIndex]);
            expect(result[CommonFakes.firstIndex].value).toEqual(result[CommonFakes.firstIndex].label);
            expect(result[CommonFakes.secondIndex].value).toEqual(CommonFakes.twoOptions[CommonFakes.secondIndex]);
            expect(result[CommonFakes.secondIndex].value).toEqual(result[CommonFakes.secondIndex].label);
        });
    });

    describe('getRatesContextData', () => {
        it('Returns the RatesContextData Object', () => {
            let sut = new GnbApp();
            sut.state.ratesVisibility = CommonFakes.visible;
            sut.state.spinnerVisibility = CommonFakes.invisible;
            sut.state.rates = CommonFakes.emptyArray;

            let result = sut.getRatesContextData();

            expect(result.ratesVisibility).toEqual(CommonFakes.visible);
            expect(result.spinnerVisibility).toEqual(CommonFakes.invisible);
            expect(result.rates).toEqual(CommonFakes.emptyArray);
        });
    });

    describe('getTransactionsContextData', () => {
        it('Returns the TransactionsContextData Object', () => {
            let sut = new GnbApp();
            sut.state.transactionsVisibility = CommonFakes.visible;
            sut.state.spinnerVisibility = CommonFakes.invisible;
            sut.state.transactions = CommonFakes.emptyArray;

            let result = sut.getTransactionsContextData();

            expect(result.transactionsVisibility).toEqual(CommonFakes.visible);
            expect(result.spinnerVisibility).toEqual(CommonFakes.invisible);
            expect(result.transactions).toEqual(CommonFakes.emptyArray);
        });
    });

    describe('getSkuSummaryContextData', () => {
        it('Returns the SkuSummaryContextData Object', () => {
            let sut = new GnbApp();
            sut.state.skuOptions = CommonFakes.oneOption;
            sut.state.skuSummaryVisibility = CommonFakes.visible;
            sut.state.spinnerVisibility = CommonFakes.invisible;
            let result = sut.getSkuSummaryContextData();

            expect(result.skuSummaryVisibility).toEqual(CommonFakes.visible);
            expect(result.spinnerVisibility).toEqual(CommonFakes.invisible);
            expect(result.transactions).toEqual(CommonFakes.emptyArray);
            expect(result.options[CommonFakes.firstIndex].value).toEqual(CommonFakes.oneOption[CommonFakes.firstIndex]);
            expect(result.options[CommonFakes.firstIndex].value).toEqual(result.options[CommonFakes.firstIndex].label);
        });
    });
});
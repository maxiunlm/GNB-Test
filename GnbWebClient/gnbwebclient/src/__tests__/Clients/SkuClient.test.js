/* global expect, spyOn */
import SkuClient from '../../Clients/SkuClient';
import ClientBase from '../../Clients/Base/ClientBase';
import { CommonFakes } from '../Fakes/CommonFakes'

describe('SkuClient', () => {
    describe('listSkus', () => {
        it('Invokes "fetch" Mothod from "ClientBase" Object once', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.oneSku);
            });
            let sut = new SkuClient();

            await sut.listSkus();

            expect(ClientBase.prototype.fetch.calls.count()).toEqual(CommonFakes.once);
        });

        it('Returns an Array with no "Skus"', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.emptyArray);
            });
            let sut = new SkuClient();

            let result = await sut.listSkus();

            expect(result.length).toEqual(CommonFakes.emptyArray.length);
            expect(result.length).toEqual(CommonFakes.zero);
        });

        it('Returns an Array with one "Sku"', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.oneSku);
            });
            let sut = new SkuClient();

            let result = await sut.listSkus();

            expect(result[CommonFakes.firstIndex]).toEqual(CommonFakes.oneSku[CommonFakes.firstIndex]);
        });

        it('Returns an Array with two "Skus"', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.twoSkus);
            });
            let sut = new SkuClient();

            let result = await sut.listSkus();

            expect(result[CommonFakes.firstIndex]).toEqual(CommonFakes.twoSkus[CommonFakes.firstIndex]);
            expect(result[CommonFakes.secondIndex]).toEqual(CommonFakes.twoSkus[CommonFakes.secondIndex]);
        });
    });

    describe('getSummaryUri', () => {
        it('With a fake Sku returns the URL for the Sku Summary', () => {
            let sut = new SkuClient();

            let result = sut.getSummaryUri(CommonFakes.faleSku);

            expect(result).toEqual(process.env.REACT_APP_summaryWebapiEndpoint + CommonFakes.faleSku);
        });
    });

    describe('getSku', () => {
        it('With a fake Sku returns the Sku Summary', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.skuInfo);
            });
            let sut = new SkuClient();

            let result = await sut.getSku(CommonFakes.faleSku);

            expect(result.name).toEqual(CommonFakes.skuInfo.name);
            expect(result.total).toEqual(CommonFakes.skuInfo.total);
            expect(result.transactions).toEqual(CommonFakes.skuInfo.transactions);
            expect(result.transactions instanceof Array).toBeTruthy();
        });
    });
});
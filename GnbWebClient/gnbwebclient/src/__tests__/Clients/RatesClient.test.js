/* global expect, spyOn */
import RatesClient from '../../Clients/RatesClient';
import ClientBase from '../../Clients/Base/ClientBase';
import { CommonFakes } from '../Fakes/CommonFakes'

describe('RatesClient', () => {
    describe('listRates', () => {
        it('Invokes "fetch" Mothod from "ClientBase" Object once', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.oneRate);
            });
            let sut = new RatesClient();

            await sut.listRates();

            expect(ClientBase.prototype.fetch.calls.count()).toEqual(CommonFakes.once);
        });

        it('Returns an Array with no "Rates"', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.emptyArray);
            });
            let sut = new RatesClient();

            let result = await sut.listRates();

            expect(result.length).toEqual(CommonFakes.emptyArray.length);
            expect(result.length).toEqual(CommonFakes.zero);
        });

        it('Returns an Array with one "Rate"', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.oneRate);
            });
            let sut = new RatesClient();

            let result = await sut.listRates();

            expect(result[CommonFakes.firstIndex].from).toEqual(CommonFakes.oneRate[CommonFakes.firstIndex].from);
            expect(result[CommonFakes.firstIndex].to).toEqual(CommonFakes.oneRate[CommonFakes.firstIndex].to);
            expect(result[CommonFakes.firstIndex].rate).toEqual(CommonFakes.oneRate[CommonFakes.firstIndex].rate);
        });

        it('Returns an Array with two "Rates"', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.twoRates);
            });
            let sut = new RatesClient();

            let result = await sut.listRates();

            expect(result[CommonFakes.firstIndex].from).toEqual(CommonFakes.twoRates[CommonFakes.firstIndex].from);
            expect(result[CommonFakes.firstIndex].to).toEqual(CommonFakes.twoRates[CommonFakes.firstIndex].to);
            expect(result[CommonFakes.firstIndex].rate).toEqual(CommonFakes.twoRates[CommonFakes.firstIndex].rate);
            expect(result[CommonFakes.secondIndex].from).toEqual(CommonFakes.twoRates[CommonFakes.secondIndex].from);
            expect(result[CommonFakes.secondIndex].to).toEqual(CommonFakes.twoRates[CommonFakes.secondIndex].to);
            expect(result[CommonFakes.secondIndex].rate).toEqual(CommonFakes.twoRates[CommonFakes.secondIndex].rate);
        });
    });
});
/* global expect, spyOn */
import TransactionsClient from '../../Clients/TransactionsClient';
import ClientBase from '../../Clients/Base/ClientBase';
import { CommonFakes } from '../Fakes/CommonFakes'

describe('TransactionsClient', () => {
    describe('listTransactions', () => {
        it('Invokes "fetch" Mothod from "ClientBase" Object once', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.oneTransaction);
            });
            let sut = new TransactionsClient();

            await sut.listTransactions();

            expect(ClientBase.prototype.fetch.calls.count()).toEqual(CommonFakes.once);
        });

        it('Returns an Array with no "Transactions"', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.emptyArray);
            });
            let sut = new TransactionsClient();

            let result = await sut.listTransactions();

            expect(result.length).toEqual(CommonFakes.emptyArray.length);
            expect(result.length).toEqual(CommonFakes.zero);
        });

        it('Returns an Array with one "Transaction"', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.oneTransaction);
            });
            let sut = new TransactionsClient();

            let result = await sut.listTransactions();

            expect(result[CommonFakes.firstIndex].sku).toEqual(CommonFakes.oneTransaction[CommonFakes.firstIndex].sku);
            expect(result[CommonFakes.firstIndex].currency).toEqual(CommonFakes.oneTransaction[CommonFakes.firstIndex].currency);
            expect(result[CommonFakes.firstIndex].amount).toEqual(CommonFakes.oneTransaction[CommonFakes.firstIndex].amount);
        });

        it('Returns an Array with two "Transactions"', async () => {
            spyOn(ClientBase.prototype, 'fetch').and.callFake(async () => {
                return Promise.resolve(CommonFakes.twoTransactions);
            });
            let sut = new TransactionsClient();

            let result = await sut.listTransactions();

            expect(result[CommonFakes.firstIndex].sku).toEqual(CommonFakes.twoTransactions[CommonFakes.firstIndex].sku);
            expect(result[CommonFakes.firstIndex].currency).toEqual(CommonFakes.twoTransactions[CommonFakes.firstIndex].currency);
            expect(result[CommonFakes.firstIndex].amount).toEqual(CommonFakes.twoTransactions[CommonFakes.firstIndex].amount);
            expect(result[CommonFakes.secondIndex].sku).toEqual(CommonFakes.twoTransactions[CommonFakes.secondIndex].sku);
            expect(result[CommonFakes.secondIndex].currency).toEqual(CommonFakes.twoTransactions[CommonFakes.secondIndex].currency);
            expect(result[CommonFakes.secondIndex].amount).toEqual(CommonFakes.twoTransactions[CommonFakes.secondIndex].amount);
        });
    });
});
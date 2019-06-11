/* global expect */
import ClientBase from '../../../Clients/Base/ClientBase';
import { CommonFakes } from '../../Fakes/CommonFakes'

describe('RatesClient', () => {
    describe('verifyResponse', () => {
        it('With a Bad response thorws an Exception', () => {
            let sut = new ClientBase();

            try {
                sut.verifyResponse(CommonFakes.responseBad);
            }
            catch (error) {
                expect(error).toEqual(CommonFakes.responseBad);
            }
        });

        it('With a Right response thorws an Exception', () => {
            let sut = new ClientBase();

            try {
                sut.verifyResponse(CommonFakes.responseOk);
                expect(true).toBeTruthy();
            }
            catch (error) {
                expect(true).toBeFalsy();
            }
        });
    });

    describe('getRequestOptions', () => {
        it('With a fake Sku returns the Sku Summary', () => {
            let sut = new ClientBase();

            let result = sut.getRequestOptions();

            expect(result.method).toEqual(CommonFakes.requestOptions.method);
            expect(result.headers instanceof Headers).toBeTruthy();
        });
    });
});
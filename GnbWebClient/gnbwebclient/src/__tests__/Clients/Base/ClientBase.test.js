/* global expect, spyOn */
import ClientBase from '../../../Clients/Base/ClientBase';
import LoginClient from '../../../Clients/LoginClient';
import { CommonFakes } from '../../Fakes/CommonFakes';

describe('ClientBase', () => {
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
            // spyOn(LoginClient.prototype, 'getAuthorizationHeader').and.callFake(async () => {
            //     return Promise.resolve(CommonFakes.oneRate);
            // });
            let loginClient = new LoginClient();
            loginClient.user = CommonFakes.user
            let sut = new ClientBase(loginClient);

            let result = sut.getRequestOptions();

            expect(result.method).toEqual(CommonFakes.requestOptions.method);
            expect(result.headers instanceof Headers).toBeTruthy();
        });
    });
});
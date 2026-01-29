/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ReviewPage } from '../models/ReviewPage';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class ReviewResource {
    /**
     * @returns any OK
     * @throws ApiError
     */
    public static pages(): CancelablePromise<Array<ReviewPage>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/umbraco/review/api/v1/pages',
            errors: {
                401: `The resource is protected and requires an authentication token`,
                403: `The authenticated user does not have access to this resource`,
            },
        });
    }
    /**
     * @returns string OK
     * @throws ApiError
     */
    public static ping(): CancelablePromise<string> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/umbraco/review/api/v1/ping',
            errors: {
                401: `The resource is protected and requires an authentication token`,
                403: `The authenticated user does not have access to this resource`,
            },
        });
    }
}

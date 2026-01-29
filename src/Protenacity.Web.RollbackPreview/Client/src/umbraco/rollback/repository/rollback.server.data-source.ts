import type { UmbControllerHost } from '@umbraco-cms/backoffice/controller-api';
import { DocumentVersionService } from '@umbraco-cms/backoffice/external/backend-api';
import { tryExecuteAndNotify } from '@umbraco-cms/backoffice/resources';

/**
 * A data source for the Rollback that fetches data from the server
 * @class UmbRollbackServerDataSource
 * @implements {RepositoryDetailDataSource}
 */
export class UmbRollbackServerDataSource {
	#host: UmbControllerHost;

	/**
	 * Creates an instance of UmbRollbackServerDataSource.
	 * @param {UmbControllerHost} host - The controller host for this controller to be appended to
	 * @memberof UmbRollbackServerDataSource
	 */
	constructor(host: UmbControllerHost) {
		this.#host = host;
	}

	/**
	 * Get a list of versions for a document
	 * @param id
	 * @param culture
	 * @returns {*}
	 * @memberof UmbRollbackServerDataSource
	 */
    getVersionsByDocumentId(id: string, culture?: string) {
        //return tryExecuteAndNotify(this.#host, DocumentVersionService.getDocumentVersion({
        //    documentId: id,
        //    culture: culture
        //}));
        // @ts-ignore
        return tryExecuteAndNotify(this.#host, DocumentVersionService.getDocumentVersion({ query: { documentId: id, culture } }))
        // @ts-check
    }

	/**
	 * Get a specific version by id
	 * @param versionId
	 * @returns {*}
	 * @memberof UmbRollbackServerDataSource
	 */
    getVersionById(versionId: string) {
        //return tryExecuteAndNotify(this.#host, DocumentVersionService.getDocumentVersionById({
        //    id: versionId
        //}));
        // @ts-ignore
        return tryExecuteAndNotify(this.#host, DocumentVersionService.getDocumentVersionById({ path: { id: versionId } }));
        // @ts-check
	}

	setPreventCleanup(versionId: string, preventCleanup: boolean) {
		//return tryExecuteAndNotify(
		//	this.#host,
		//	DocumentVersionService.putDocumentVersionByIdPreventCleanup({
        //              id: versionId,
        //              preventCleanup: preventCleanup
		//	}),
        //);
        // @ts-ignore
        return tryExecuteAndNotify(this.#host,DocumentVersionService.putDocumentVersionByIdPreventCleanup({path: { id: versionId },query: { preventCleanup }}));
        // @ts-check

	}

	rollback(versionId: string, culture?: string) {
		//return tryExecuteAndNotify(
		//	this.#host,
  //          DocumentVersionService.postDocumentVersionByIdRollback({
  //              id: versionId,
  //              culture: culture
  //          })
        //);

        // @ts-ignore
        return tryExecuteAndNotify(this.#host,DocumentVersionService.postDocumentVersionByIdRollback({ path: { id: versionId }, query: { culture } }));
        // @ts-check


	}
}
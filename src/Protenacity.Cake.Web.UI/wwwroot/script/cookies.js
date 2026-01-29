import * as CookieConsent from 'https://cdn.jsdelivr.net/gh/orestbida/cookieconsent@3.0.1/dist/cookieconsent.esm.js';

(function () {

    const CAT_NECESSARY = "necessary";
    const CAT_ANALYTICS = "analytics";
    const CAT_BESPOKE = "bespoke";

    const SERVICE_AD_STORAGE = 'ad_storage'
    const SERVICE_AD_USER_DATA = 'ad_user_data'
    const SERVICE_AD_PERSONALIZATION = 'ad_personalization'
    const SERVICE_ANALYTICS_STORAGE = 'analytics_storage'
    const SERVICE_FUNCTIONALITY_STORAGE = 'functionality_storage'
    const SERVICE_PERSONALIZATION_STORAGE = 'personalization_storage'
    const SERVICE_SECURITY_STORAGE = 'security_storage'

    const model = JSON.parse(document.querySelector('script[data-cookies]').getAttribute('data-cookies'));

    function gtag() {
        dataLayer.push(arguments);
    }

    if (model.integrateWithGoogleTagManager) {
        window.dataLayer = window.dataLayer || [];
        gtag('consent', 'default', {
            [SERVICE_AD_STORAGE]: 'denied',
            [SERVICE_AD_USER_DATA]: 'denied',
            [SERVICE_AD_PERSONALIZATION]: 'denied',
            [SERVICE_ANALYTICS_STORAGE]: 'denied',
            [SERVICE_FUNCTIONALITY_STORAGE]: 'denied',
            [SERVICE_PERSONALIZATION_STORAGE]: 'denied',
            [SERVICE_SECURITY_STORAGE]: 'denied',
        });
    }

    function updateGtagConsent() {
        if (model.integrateWithGoogleTagManager) {
            gtag('consent', 'update', {
                [SERVICE_ANALYTICS_STORAGE]: CookieConsent.acceptedService(SERVICE_ANALYTICS_STORAGE, CAT_ANALYTICS) ? 'granted' : 'denied',
                [SERVICE_AD_STORAGE]: CookieConsent.acceptedService(SERVICE_AD_STORAGE, CAT_BESPOKE) ? 'granted' : 'denied',
                [SERVICE_AD_USER_DATA]: CookieConsent.acceptedService(SERVICE_AD_USER_DATA, CAT_BESPOKE) ? 'granted' : 'denied',
                [SERVICE_AD_PERSONALIZATION]: CookieConsent.acceptedService(SERVICE_AD_PERSONALIZATION, CAT_BESPOKE) ? 'granted' : 'denied',
                [SERVICE_FUNCTIONALITY_STORAGE]: CookieConsent.acceptedService(SERVICE_FUNCTIONALITY_STORAGE, CAT_NECESSARY) ? 'granted' : 'denied',
                [SERVICE_PERSONALIZATION_STORAGE]: CookieConsent.acceptedService(SERVICE_PERSONALIZATION_STORAGE, CAT_NECESSARY) ? 'granted' : 'denied',
                [SERVICE_SECURITY_STORAGE]: CookieConsent.acceptedService(SERVICE_SECURITY_STORAGE, CAT_NECESSARY) ? 'granted' : 'denied'
            });
        }
    }

    function decodeHtml(html) {
        var txt = document.createElement('textarea');
        txt.innerHTML = html;
        return txt.value;
    }

    function cookieTable(c) {
        var results = [];
        for (var i = 0; i != c.length; i++) {
            results.push({
                name: c[i].content.cookieName,
                domain: c[i].content.cookieDomain,
                expire: c[i].content.cookieExpiration,
                desc: decodeHtml(c[i].content.cookieDescription)
            })
        }

        return {
            caption: '',
            headers: {
                name: 'Cookie',
                domain: 'Domain',
                expire: 'Expire',
                desc: 'Description'
            },
            body: results
        };
    }

    function categories() {
        var cats = {};
        cats[CAT_NECESSARY] = {
            enabled: true,
            readOnly: true,
            services: model.integrateWithGoogleTagManager ? {
                [SERVICE_FUNCTIONALITY_STORAGE]: {
                    label: 'Enables storage that supports the functionality of the website or app e.g. language settings.'
                },
                [SERVICE_PERSONALIZATION_STORAGE]: {
                    label: 'Enables storage related to personalization e.g. video recommendations.'
                },
                [SERVICE_SECURITY_STORAGE]: {
                    label: 'Enables storage related to security such as authentication functionality, fraud prevention, and other user protection.'
                }
            } : {},
        };

        if (model.analyticEnable) {
            cats[CAT_ANALYTICS] = {
                enabled: model.analyticEnable,
                readOnly: false,
                services: model.integrateWithGoogleTagManager ? {
                    [SERVICE_ANALYTICS_STORAGE]: {
                        label: 'Enables storage (such as cookies) related to analytics e.g. visit duration.'
                    }
                } : {},
            };
        }

        if (model.bespokeEnable) {
            cats[CAT_BESPOKE] = {
                enabled: model.bespokeEnable,
                readOnly: false,
                services: model.integrateWithGoogleTagManager ? {
                    [SERVICE_AD_STORAGE]: {
                        label: 'Enables storage (such as cookies) related to advertising.'
                    },
                    [SERVICE_AD_USER_DATA]: {
                        label: 'Sets consent for sending user data related to advertising to Google.'
                    },
                    [SERVICE_AD_PERSONALIZATION]: {
                        label: 'Sets consent for personalized advertising.'
                    }
                } : {},
            };
        }
        return cats;
    }

    window.addEventListener('DOMContentLoaded', () => {
        try {
            if (model) {
                if (model.backgroundColor == '#000000') {
                    document.documentElement.classList.add('cc--darkmode');
                }

                CookieConsent.run({
                    onFirstConsent: () => {
                        updateGtagConsent();
                    },
                    onConsent: () => {
                        updateGtagConsent();
                    },
                    onChange: () => {
                        updateGtagConsent();
                    },
                    guiOptions: {
                        consentModal: {
                            layout: 'cloud inline',
                            position: model.modalPosition.toLowerCase() ?? 'bottom center',
                            equalWeightButtons: true,
                            flipButtons: false
                        },
                        preferencesModal: {
                            layout: 'box',
                            equalWeightButtons: true,
                            flipButtons: false
                        }
                    },
                    categories: categories(),
                    language: {
                        default: 'en',
                        translations: {
                            'en': {
                                consentModal: {
                                    title: model.modalTitle,
                                    description: decodeHtml(model.modalDescription),
                                    acceptAllBtn: model.modalAcceptAll,
                                    acceptNecessaryBtn: model.modalRejectAll,
                                    showPreferencesBtn: model.modalCustom
                                },
                                preferencesModal: {
                                    title: model.customTitle,
                                    acceptAllBtn: model.modalAcceptAll,
                                    acceptNecessaryBtn: model.modalRejectAll,
                                    savePreferencesBtn: model.cookieConsentCustomSave,
                                    closeIconLabel: model.customClose,
                                    sections: [
                                        {
                                            title: model.customSectionTitle,
                                            description: decodeHtml(model.customSectionDescription)
                                        },
                                        {
                                            title: model.necessaryTitle,
                                            description: decodeHtml(model.necessaryDescription),
                                            linkedCategory: [CAT_NECESSARY],
                                            cookieTable: cookieTable(model.necessaryCookieList)
                                        },
                                        {
                                            title: model.analyticTitle,
                                            description: decodeHtml(model.analyticDescription),
                                            linkedCategory: [CAT_ANALYTICS],
                                            cookieTable: cookieTable(model.analyticCookieList)
                                        },
                                        {
                                            title: model.bespokeTitle,
                                            description: decodeHtml(model.bespokeDescription),
                                            linkedCategory: [CAT_BESPOKE],
                                            cookieTable: cookieTable(model.bespokeCookieList)
                                        },
                                        {
                                            title: model.customMoreInfoTitle,
                                            description: decodeHtml(model.customMoreInfoDescription)
                                        }
                                    ]
                                }
                            }
                        }
                    }
                });
            }
        }
        catch (ex) {
            console.log(ex);
            // We have no cookies, so ignore
        }
    });
})();


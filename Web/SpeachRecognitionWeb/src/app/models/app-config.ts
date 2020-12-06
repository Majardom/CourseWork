export interface IAppConfig {
    authenticationService : {
        endpoint: string;
        authenticatonTokenEndpoint: string;

        restful: {
            validateToken: string;
        }
    }
}
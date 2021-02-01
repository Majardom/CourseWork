export interface IAppConfig {
    authenticationService : {
        endpoint: string;
        authenticatonTokenEndpoint: string;

        restful: {
            validateToken: string;
        }
    }

    voiceRecognitionService: {
        endpoint: string,

        restful: {
            addVoiceSample: string,
            identify: string
        }
    }
}
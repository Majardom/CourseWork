import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfigService } from './app-config.service';

interface SampleDto 
{
  samplesBase64: string[];
}

@Injectable({
  providedIn: 'root'
})
export class RecognitionService {

  constructor(private _http: HttpClient) { }

  public addVoiceSamples(author: string, samplesBase64: string[]) {
    let authenticationUrl = `${AppConfigService.settings.voiceRecognitionService.endpoint}/${AppConfigService.settings.voiceRecognitionService.restful.addVoiceSample}${author}`;
    
    let dto: SampleDto = {
      samplesBase64: samplesBase64
    }
    
    return this._http.post<object>(authenticationUrl, dto);
  }

  public identifyUser(sampleBase64: string)
  {
    let identifyUrl = `${AppConfigService.settings.voiceRecognitionService.endpoint}/${AppConfigService.settings.voiceRecognitionService.restful.identify}`;

    let dto: SampleDto = {
      samplesBase64: [sampleBase64]
    }

    return this._http.post<string>(identifyUrl, dto);
  }
}

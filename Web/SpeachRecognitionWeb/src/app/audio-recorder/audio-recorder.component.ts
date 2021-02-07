import { Component, OnInit } from '@angular/core';


import * as RecordRTC from 'recordrtc';
import { DomSanitizer } from '@angular/platform-browser';
import { RecognitionService } from '../services/recognition.service';
import { promise } from 'protractor';
import { Input } from '@angular/core';
declare var $: any;

interface IBlobData {
  blob: Blob;
  url: string;
}

@Component({
  selector: 'app-audio-recorder',
  templateUrl: './audio-recorder.component.html',
  styleUrls: ['./audio-recorder.component.scss']
})
export class AudioRecorderComponent implements OnInit {
  title = 'micRecorder';
  //Lets declare Record OBJ
  record: any;
  //Will use this flag for toggeling recording
  recording = false;
  //URL of Blob
  url;

  public blobs: IBlobData[] = [];

  error;

  @Input()
  public authorName: string;

  @Input()
  public identify: boolean;

  public identifyRecordUrl: string;
  public identifyRecordBlob: Blob;

  constructor(private domSanitizer: DomSanitizer, private _recognitionService: RecognitionService) { }
  sanitize(url: string) {
    return this.domSanitizer.bypassSecurityTrustUrl(url);
  }
  /**
  * Start recording.
  */
  initiateRecording() {
    this.recording = true;
    let mediaConstraints = {
      video: false,
      audio: true
    };
    navigator.mediaDevices.getUserMedia(mediaConstraints).then(this.successCallback.bind(this), this.errorCallback.bind(this));


  }
  /**
  * Will be called automatically.
  */
  successCallback(stream) {
    var options = {
      mimeType: "audio/wav",
      numberOfAudioChannels: 2,
      sampleRate: 44100,
    };
    //Start Actuall Recording
    var StereoAudioRecorder = RecordRTC.StereoAudioRecorder;
    this.record = new StereoAudioRecorder(stream, options);
    this.record.record();
  }
  /**
  * Stop recording.
  */
  stopRecording() {
    this.recording = false;
    this.record.stop(this.processRecording.bind(this));

  }
  /**
  * processRecording Do what ever you want with blob
  * @param  {any} blob Blog
  */
  processRecording(blob) {
    this.url = URL.createObjectURL(blob);
    console.log("blob", blob);
    console.log("url", this.url);
    if (!this.identify) {
      this.blobs.push({ blob: blob, url: this.url });
    }
    else  {
      this.identifyRecordUrl = this.url;
      this.identifyRecordBlob = blob;
    }
  }
  /**
  * Process Error.
  */
  errorCallback(error) {
    this.error = 'Can not play audio in your browser';
  }

  deleteVoiceSample(index: number) {
    this.blobs.splice(index, 1);
  }

  public async saveVoiceSamples(): Promise<void> {
    if (this.authorName == null)
      return;

    var samplesBase64 = [];

    for (let blob of this.blobs) {
      var base64String = await this._blobToBase64(blob.blob);
      samplesBase64.push((<string>base64String));
    }

    this._recognitionService.addVoiceSamples(this.authorName, samplesBase64).subscribe(() => {
      console.log("OK")
    },
      (error) => { 
        console.log(error)
      });

    this.blobs = [];
    this.authorName = "";
  }

  public async identifyUser()
  {
      let base64 = await this._blobToBase64(this.identifyRecordBlob);
      this._recognitionService.identifyUser(<string>base64).subscribe((result) => {
       alert(result);
      }, (error) => { 
        console.log(error)
      })
  }

  public deleteIdentify()
  {
    this.identifyRecordUrl = null;
  }

  ngOnInit() { }

  private _blobToBase64(blob: Blob): Promise<string> {
    return new Promise<string>(resolve => {
      var reader = new FileReader();
      reader.readAsDataURL(blob);
      reader.onloadend = () => {
        var base64data = reader.result;
        resolve((<string>base64data).split(',')[1]);
      }
    });
  }

}

import { FileUploader } from 'ng2-file-upload';

export class MyUploader extends FileUploader {
  isHTML5 = true;  

  constructor(options: any) {
    super(options);
  }
}

/*
Modified FileUploader to expose isHTML5 attribute
-its set to private in the package and was causing issues 
-The uploader needs to run in HTML5 mode to attach the native File object, 
which contains the .size and ,progress property, 
In newer versions, ng2-file-upload made properties like isHTML5 internal or private-like, so:

Setting isHTML5 in the config doesn’t expose it as uploader.isHTML5

If HTML5 mode isn’t detected/enforced, you only get minimal file metadata
*/
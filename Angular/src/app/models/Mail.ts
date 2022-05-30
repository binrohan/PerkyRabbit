import { BaseModel } from './BaseModel';

export interface Mail extends BaseModel {
  to: string;
  cc: string;
  bcc: string;
  subject: string;
  body: string;
}

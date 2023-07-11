const { GoogleAuth } = require('google-auth-library');
const jwt = require('jsonwebtoken');
const { v4: uuidv4 } = require('uuid');

// TODO: Define issuer ID
let issuerId = '3388000000022214670';
let classSuffix = 'amil2';
let objectSuffix = uuidv4();
const objectId = `${issuerId}.${objectSuffix}`;
const keyFilePath = process.env.GOOGLE_APPLICATION_CREDENTIALS || '/Users/brunoferreira/Documents/NewGoogleWallet/NewGoogleWallet/Scripts/projetowalletamil-72f77c93e76e.json';

const baseUrl = 'https://walletobjects.googleapis.com/walletobjects/v1';

const credentials = require(keyFilePath);

const httpClient = new GoogleAuth({
  credentials: credentials,
  scopes: 'https://www.googleapis.com/auth/wallet_object.issuer'
});

// Create a Generic pass object
let genericObject = {
  'id': `${objectId}`,
  'classId': `${issuerId}.${classSuffix}`,
  'genericType': 'GENERIC_TYPE_UNSPECIFIED',
  'hexBackgroundColor': '#004f98',
  'logo': {
    'sourceUri': {
      'uri': 'https://storage.googleapis.com/wallet-lab-tools-codelab-artifacts-public/pass_google_logo.jpg'
    }
  },
  'cardTitle': {
    'defaultValue': {
      'language': 'en-US',
      'value': 'Amil'
    }
  },
  'subheader': {
    'defaultValue': {
      'language': 'en-US',
      'value': 'Número do beneficiário'
    }
  },
  'header': {
    'defaultValue': {
      'language': 'en-US',
      'value': '078385846'
    }
  },
  'textModulesData': [
    {
      'header': 'Nome',
      'body': 'Francine Gatis',
      'id': 'nome'
    },
    {
      'header': 'Rede',
      'body': 'AMIL S750 COLAB',
      'id': 'rede'
    }
  ]
}

const genericClass = require('./generic_class.js');

const claims = {
  iss: credentials.client_email, // `client_email` in service account file.
  aud: 'google',
  origins: ['http://localhost:3000'],
  typ: 'savetowallet',
  payload: {
    genericObjects: [genericObject],
    genericClasses: [genericClass],
  },
};

const token = jwt.sign(claims, credentials.private_key, {algorithm: 'RS256'});
console.log(token)

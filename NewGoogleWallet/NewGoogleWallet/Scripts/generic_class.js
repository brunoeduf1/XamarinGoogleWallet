const { GoogleAuth } = require('google-auth-library');

// TODO: Define issuer ID
let issuerId = '3388000000022214670';
let classSuffix = 'amil2';
const classId = `${issuerId}.${classSuffix}`;
const keyFilePath = process.env.GOOGLE_APPLICATION_CREDENTIALS || '/Users/brunoferreira/Documents/NewGoogleWallet/NewGoogleWallet/Scripts/projetowalletamil-72f77c93e76e.json';

const baseUrl = 'https://walletobjects.googleapis.com/walletobjects/v1';

const credentials = require(keyFilePath);

const httpClient = new GoogleAuth({
  credentials: credentials,
  scopes: 'https://www.googleapis.com/auth/wallet_object.issuer'
});

// Create a Generic pass class
let genericClass = {
  'id': `${classId}`,
  'classTemplateInfo': {
    'cardTemplateOverride': {
      'cardRowTemplateInfos': [
        {
          'twoItems': {
            'startItem': {
              'firstValue': {
                'fields': [
                  {
                    'fieldPath': 'object.textModulesData["nome"]',
                  },
                ],
              },
            },
            'endItem': {
              'firstValue': {
                'fields': [
                  {
                    'fieldPath': 'object.textModulesData["rede"]',
                  },
                ],
              },
            },
          },
        },
      ],
    }
  }
};

//genericClass = genericClass.replace(/\\/g, '');

module.exports = genericClass

// Check if the class exists already
httpClient.request({
  url: `${baseUrl}/genericClass/${classId}`,
  method: 'GET',
}).then(response => {
  console.log('Class already exists');
  console.log(response);

  console.log('Class ID');
  console.log(response.data.id);
}).catch(err => {
  if (err.response && err.response.status === 404) {
    // Class does not exist
    // Create it now
    httpClient.request({
      url: `${baseUrl}/genericClass`,
      method: 'POST',
      data: genericClass,
    }).then(response => {
      console.log('Class insert response');
      console.log(response);

      console.log('Class ID');
      console.log(response.data.id);
    });
  } else {
    // Something else went wrong
    console.log(err);
  }
});

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NewGoogleWallet
{
    public class WalletObject
    {
        public string issuerEmail { get; set; }
        public string issuerId { get; set; }
        public string passClass { get; set; }
        public string passId { get; set; }
    }

    public class SourceUri
    {
        public string uri { get; set; }
    }

    public class ContentDescription
    {
        public DefaultValue defaultValue { get; set; }
    }

    public class DefaultValue
    {
        public string language { get; set; }
        public string value { get; set; }
    }

    public class Logo
    {
        public SourceUri sourceUri { get; set; }
        public ContentDescription contentDescription { get; set; }
    }

    public class CardTitle
    {
        public DefaultValue defaultValue { get; set; }
    }

    public class Subheader
    {
        public DefaultValue defaultValue { get; set; }
    }

    public class Header
    {
        public DefaultValue defaultValue { get; set; }
    }

    public class TextModulesData
    {
        public string id { get; set; }
        public string header { get; set; }
        public string body { get; set; }
    }

    public class GenericObject
    {
        public string id { get; set; }
        public string classId { get; set; }
        public Logo logo { get; set; }
        public CardTitle cardTitle { get; set; }
        public Subheader subheader { get; set; }
        public Header header { get; set; }
        public string hexBackgroundColor { get; set; }
        public List<TextModulesData> textModulesData { get; set; }
    }

    public class Field
    {
        public string fieldPath { get; set; }
    }

    public class FirstValue
    {
        public List<Field> fields { get; set; }
    }

    public class CardRowTemplateInfos
    {
        public TwoItems twoItems { get; set; }
    }

    public class TwoItems
    {
        public FirstValue startItem { get; set; }
        public FirstValue endItem { get; set; }
    }

    public class CardTemplateOverride
    {
        public List<CardRowTemplateInfos> cardRowTemplateInfos { get; set; }
    }

    public class ClassTemplateInfo
    {
        public CardTemplateOverride cardTemplateOverride { get; set; }
    }

    public class GenericClass
    {
        public string id { get; set; }
        public ClassTemplateInfo classTemplateInfo { get; set; }
    }

    public class Payload
    {
        public List<GenericObject> genericObjects { get; set; }
        public List<GenericClass> genericClasses { get; set; }
    }

    public class RootObject
    {
        public string iss { get; set; }
        public string aud { get; set; }
        public string typ { get; set; }
        public long iat { get; set; }
        public List<string> origins { get; set; }
        public Payload payload { get; set; }
    }

    public static class WalletObjectConverter
    {
        public static string NewObjectJson(WalletObject walletObject)
        {
            string issuerEmail = walletObject.issuerEmail;
            string issuerId = walletObject.issuerId;
            string passClass = walletObject.passClass;
            string passId = walletObject.passId;

            RootObject newObjectJson = new RootObject
            {
                payload = new Payload
                {
                    genericObjects = new List<GenericObject>
                    {
                        new GenericObject
                        {
                            id = $"{issuerId}.{passId}",
                            classId = $"{issuerId}.{passClass}",
                            logo = new Logo
                            {
                                sourceUri = new SourceUri
                                {
                                    uri = "https://storage.googleapis.com/wallet-lab-tools-codelab-artifacts-public/pass_google_logo.jpg"
                                },
                                contentDescription = new ContentDescription
                                {
                                    defaultValue = new DefaultValue
                                    {
                                        language = "pt-BR",
                                        value = "LOGO_IMAGE_DESCRIPTION"
                                    }
                                }
                            },
                            cardTitle = new CardTitle
                            {
                                defaultValue = new DefaultValue
                                {
                                    language = "pt-BR",
                                    value = "Amil"
                                }
                            },
                            subheader = new Subheader
                            {
                                defaultValue = new DefaultValue
                                {
                                    language = "pt-BR",
                                    value = "Número do beneficiário"
                                }
                            },
                            header = new Header
                            {
                                defaultValue = new DefaultValue
                                {
                                    language = "pt-BR",
                                    value = "078385846"
                                }
                            },
                            hexBackgroundColor = "#004f98",
                            textModulesData = new List<TextModulesData>
                            {
                                new TextModulesData
                                {
                                    id = "nome",
                                    header = "Nome",
                                    body = "Francine Gatis"
                                },
                                new TextModulesData
                                {
                                    id = "rede",
                                    header = "Rede",
                                    body = "AMIL S750 COLAB"
                                }
                            }
                        }
                    },
                    genericClasses = new List<GenericClass>
                    {
                        new GenericClass
                        {
                            id = $"{issuerId}.{passClass}",
                            classTemplateInfo = new ClassTemplateInfo
                            {
                                cardTemplateOverride = new CardTemplateOverride
                                {
                                    cardRowTemplateInfos = new List<CardRowTemplateInfos>
                                    {
                                        new CardRowTemplateInfos
                                        {
                                            twoItems = new TwoItems
                                            {
                                                startItem = new FirstValue
                                                {
                                                    fields = new List<Field>
                                                    {
                                                        new Field
                                                        {
                                                            fieldPath = "object.textModulesData['nome']"
                                                        }
                                                    }
                                                },
                                                endItem = new FirstValue
                                                {
                                                    fields = new List<Field>
                                                    {
                                                        new Field
                                                        {
                                                            fieldPath = "object.textModulesData['rede']"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            string newObjectJsonString = JsonConvert.SerializeObject(newObjectJson);

            return newObjectJsonString;
        }
    }
}


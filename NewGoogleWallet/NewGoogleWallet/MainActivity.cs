using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using AndroidX.AppCompat.App;
using Com.Example.Nativelib;

namespace NewGoogleWallet
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public class ButtonClickListener : Java.Lang.Object, View.IOnClickListener
        {
            Context context;

            public ButtonClickListener(Context context)
            {
                this.context = context;
            }

            public void OnClick(View v)
            {
                MainActivity main = new MainActivity();

                main.FabOnClick(context);
            }
        }

        string walletIssuerEmail = "xxxxxxxxxxx@xxxxxxxxxxxxx.iam.gserviceaccount.com";
        string walletIssuerId = "33880000000xxxxxxxx";
        string walletPassClass = "amil";
        string walletUserName = "Bruno Ferreira";
        string walletUserNetwork = "Teste";
        string walletOpticalMark = "123456789";
        string walletBackgroundColor = "#004f98";
        string walletTitle = "Teste";
        string walletUriLogo = "https://storage.googleapis.com/wallet-lab-tools-codelab-artifacts-public/pass_google_logo.jpg";
        string walletPassId = Guid.NewGuid().ToString();
        WalletActivity wallet = new WalletActivity();
        FloatingActionButton fab;
        bool isWalletInstalled;
        private static bool retFromWalletActivity = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            OpenNativeActivity(this);

            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.SetOnClickListener(new ButtonClickListener(this));
        }

        private void FabOnClick(Context context)
        {
            var teste = WalletJsonObject();
            wallet.AddToWallet(context, teste);
            Console.WriteLine(teste);
        }

        async void VerifyWalletAvailable()
        {
            for (int i = 0; i < 8; i++)
            {
                isWalletInstalled = wallet.IsWalletInstalled;
                if (isWalletInstalled)
                {
                    fab.Visibility = ViewStates.Visible;
                    break;
                }

                await System.Threading.Tasks.Task.Delay(250);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (retFromWalletActivity)
            {
                VerifyWalletAvailable();
                retFromWalletActivity = false;
            }

            retFromWalletActivity = true;
        }

        private string WalletJsonObject()
        {
            DateTimeOffset epochStart = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            long iatTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            string genericObject =
              @"
              {
              ""iss"": """ + walletIssuerEmail + @""",
              ""aud"": ""google"",
              ""typ"": ""savetowallet"",
              ""iat"": """ + iatTimestamp + @""",
              ""origins"": [
                ""www.example.com""
              ],
              ""payload"": {
                ""genericObjects"": [
                      {
                        ""id"": """ + walletIssuerId + "." + walletPassId + @""",
                        ""classId"": """ + walletIssuerId + "." + walletPassClass + @""",
                        ""logo"": {
                          ""sourceUri"": {
                            ""uri"": """ + walletUriLogo + @"""
                          },
                          ""contentDescription"": {
                            ""defaultValue"": {
                              ""language"": ""pt-BR"",
                              ""value"": ""LOGO_IMAGE_DESCRIPTION""
                            }
                          }
                        },
                        ""cardTitle"": {
                          ""defaultValue"": {
                            ""language"": ""pt-BR"",
                            ""value"": """ + walletTitle + @"""
                          }
                        },
                        ""subheader"": {
                          ""defaultValue"": {
                            ""language"": ""pt-BR"",
                            ""value"": ""Nome""
                          }
                        },
                        ""header"": {
                          ""defaultValue"": {
                            ""language"": ""pt-BR"",
                            ""value"": """ + walletUserName + @"""
                          }
                        },
                        ""hexBackgroundColor"": """ + walletBackgroundColor + @""",
                        ""textModulesData"": [
                          {
                            ""id"": ""numero"",
                            ""header"": ""Número do beneficiário"",
                            ""body"": """ + walletOpticalMark + @"""
                          },
                          {
                            ""id"": ""rede"",
                            ""header"": ""Rede"",
                            ""body"": """ + walletUserNetwork + @"""
                          }
                        ]
                      }
                    ]
                  }
                }";

            return genericObject;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void OpenNativeActivity(Context context)
        {
            var intent = new Intent(context, typeof(WalletActivity));
            StartActivity(intent);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}


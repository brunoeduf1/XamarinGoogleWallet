# Google Wallet for Xamarin Android

So far, there are no libraries in Xamarin that allow adding a generic card to the Google Wallet. However, there is an alternative, using a custom native library made in Kotlin to be imported into Xamarin.

In this project I will show how to add a generic wallet to Google Wallet using Xamarin Android.

## 1 Create a Bindings library type project

<img width="523" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/e77b1463-33d6-4af7-b97e-d895dc856271">

Click in Continue button.

<img width="885" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/22b40eab-23cf-4677-bb76-60dbc6050dbd">

Choose the project name and click in Create.

<img width="889" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/fd79d919-4b2b-4f8f-84d8-21edd63a9434">

## 2 Add the reference to the Android project

Right-click references and click add reference.

<img width="325" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/b922342d-ac7e-4b20-81a5-51e0f257157b">

Select the created project and click select.

<img width="978" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/d5dcb514-6481-4a6b-8896-062f7b5013f8">

The reference will appear as below.

<img width="278" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/a0a58684-e216-4497-b35d-e13daf53d67f">

## 3 Add the .aar file to the library

Add the nativelib-debug.aar file inside the Jars folder.

<img width="226" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/8caed746-6431-4b04-b95a-c1558d98cead">

This file was generated using Android Studio in the Kotlin language, based on the code in the link below:

https://github.com/google-pay/wallet-android-codelab/blob/main/android_complete/app/src/main/java/com/google/android/gms/samples/wallet/activity/CheckoutActivity.kt

In this file were created the methods below that will be used in the Xamarin Android project.

<img width="461" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/033c75c6-b9a8-4efa-a3ea-9988e1282a0f">

 ## 4 Add the native library

Insert the nativelib library into the Android project (using Com.Example.Nativelib)

 <img width="300" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/b3353db3-05a2-4413-8459-d35480df2040">

 ## 5 Make sure the google wallet app is installed

 Create an instance of wallet activity

 <img width="1012" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/9da86da0-f4a8-42e7-bcfa-30251fe84b2b">

 Call OpenNativeActivity inside OnCreate

 <img width="880" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/561b1778-1ab2-4e1c-b720-9001ed6114a8">

 <img width="458" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/1577447d-71be-401d-aecc-05fcee14f6f7">

 And inside OnResume, make sure the Google Wallet app is installed.

 <img width="416" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/55fd02b4-ea9a-45c1-8928-e5fcd1a3d8a9">

To verify that the application is installed, it was necessary to open the WalletActivity screen, because the method that performs this verification is a task that only works within the native project. What happens here is, the screen created in the project with Kotlin language is opened, it checks if the application is installed and saves the data in a static variable and closes this activity, this variable cannot be accessed immediately within Xamarin, and that's why we use a loop in the VerifyWalletAvailable() method to get its value.

## 6 Add card to wallet

Create the jsonObject like this

<img width="494" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/9b81d581-4a6e-4832-b75a-fb7151856380">

With parameters similar to this

<img width="932" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/d42755a0-3919-4a93-bbe8-318a6a4719b3">

In the wallet button click event, call the AddToWallet() method

<img width="349" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/ac847560-e6dc-439a-b67c-623963b6a771">

Clicking the button will show this screen

<img width="409" alt="image" src="https://github.com/brunoeduf1/XamarinGoogleWallet/assets/69606316/71694ecc-bb6a-49f2-aaf0-994ce5541bfb">











 





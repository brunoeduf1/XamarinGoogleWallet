package com.example.nativelib

import android.app.Activity
import android.content.Context
import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.nativelib.databinding.ActivityCheckoutBinding
import com.google.android.gms.pay.Pay
import com.google.android.gms.pay.PayApiAvailabilityStatus
import com.google.android.gms.pay.PayClient
import java.util.*

/**
 * Checkout implementation for the app
 */
class WalletActivity : AppCompatActivity() {


    companion object {
        var RESULT_CODE = false
    }

    private lateinit var layout: ActivityCheckoutBinding

    // TODO: Add a request code for the save operation
    private val addToGoogleWalletRequestCode = 999

    // TODO: Create a client to interact with the Google Wallet API
    lateinit var walletClient: PayClient

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        // TODO: Instantiate the Pay client
        walletClient = Pay.getClient(this)

        // Use view binding to access the UI elements
        layout = ActivityCheckoutBinding.inflate(layoutInflater)
        setContentView(layout.root)

        // TODO: Check if the Google Wallet API is available
        fetchCanUseGoogleWalletApi()

        finish()
    }

    fun IsWalletInstalled():Boolean{
        return RESULT_CODE
    }

    fun addToWalletJWT(context: Context, tokenJWT: String)
    {
        walletClient = Pay.getClient(context)
        walletClient.savePassesJwt(
            tokenJWT,
            context as Activity,
            addToGoogleWalletRequestCode
        )
    }

    fun addToWallet(context: Context, jsonObject: String)
    {
        walletClient = Pay.getClient(context)
        walletClient.savePasses(
            jsonObject,
            context as Activity,
            addToGoogleWalletRequestCode
        )
    }

    // TODO: Create a method to check for the Google Wallet SDK and API
    private fun fetchCanUseGoogleWalletApi(){
        walletClient
            .getPayApiAvailabilityStatus(PayClient.RequestType.SAVE_PASSES)
            .addOnSuccessListener { status ->
                if (status == PayApiAvailabilityStatus.AVAILABLE)
                {
                    RESULT_CODE = true
                }
            }
            .addOnFailureListener {
                // Hide the button and optionally show an error message
            }
    }

    // TODO: Handle the result
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (requestCode == addToGoogleWalletRequestCode) {
            when (resultCode) {
                RESULT_OK -> {
                    // Pass saved successfully. Consider informing the user.
                }
                RESULT_CANCELED -> {
                    // Save canceled
                }

                PayClient.SavePassesResult.SAVE_ERROR -> data?.let { intentData ->
                    val errorMessage = intentData.getStringExtra(PayClient.EXTRA_API_ERROR_MESSAGE)
                    // Handle error. Consider informing the user.
                }

                else -> {
                    // Handle unexpected (non-API) exception
                }
            }
        }
    }
}
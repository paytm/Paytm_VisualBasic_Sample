<div class="container text-center">
    <div class="shadow p-3 mb-5 bg-white rounded">
        <h2>Paytm Blink Checkout - Visual Basic</h2>
        <h4>Make Payment</h4>
        <p>You are making payment of ₹1</p>
        <div class="btn-area">
            <button type="button" id="JsCheckoutPayment" name="submit" class="btn btn-primary">Pay Now</button>
        </div>
    </div>
</div>
<script type="application/javascript" crossorigin="anonymous" src="@ViewBag.jsinvokelink"></script>
<script>
    document.getElementById("JsCheckoutPayment").addEventListener("click", function () {
        openJsCheckoutPopup("@ViewBag.orderId", "@ViewBag.txnToken", "@ViewBag.amount");
    });

    function openJsCheckoutPopup(orderId, txnToken, amount) {
  var config = {
    "root": "",
    "flow": "DEFAULT",
    "data": {
      "orderId": orderId,
      "token": txnToken,
      "tokenType": "TXN_TOKEN",
      "amount": amount
    },
    "merchant":{
      "redirect": true
    },
    "handler": {
      "notifyMerchant": function(eventName,data){
        console.log("notifyMerchant handler function called");
        console.log("eventName => ",eventName);
        console.log("data => ",data);
      }
    }
  };
  if(window.Paytm && window.Paytm.CheckoutJS){
    // initialze configuration using init method
    window.Paytm.CheckoutJS.init(config).then(function onSuccess() {
      // after successfully updating configuration, invoke checkoutjs
      window.Paytm.CheckoutJS.invoke();
    }).catch(function onError(error){
         console.log("error => ",error);
    });
  }
}


</script>
using UnityEngine;
using System.Collections;

public class BillingManager : MonoBehaviour 
{
	public static bool _isInited;

	private const string		PANDA		= "panda";
	private const string		LUGG		= "lugg";
	private const string		SPEEDY		= "demon";

	// Use this for initialization
	void Start () 
	{
		NotificationCentre.AddObserver (this, "PurchaseCharacter");

		AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased; 
		AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;

		AndroidInAppPurchaseManager.instance.loadStore();
	}
	
	private static void OnProductPurchased(BillingResult result) 
	{
		if(result.isSuccess) 
		{
			OnProcessingPurchasedProduct (result.purchase);
		}
	}

	private static void OnProcessingPurchasedProduct(GooglePurchaseTemplate purchase) 
	{
		switch(purchase.SKU)
		{
		case "panda":
			SingletonSaveData.instance.PandaLocked = false;
			break;

		case "lugg":
			SingletonSaveData.instance.JuggLocked = false;
			break;

		case "demon":
			SingletonSaveData.instance.DemonLocked = false;
			break;
		}
	}

	private static void OnBillingConnected(BillingResult result) 
	{
		AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;

		if(result.isSuccess) 
		{
			//Store connection is Successful. Next we loading product and customer purchasing details
			AndroidInAppPurchaseManager.instance.retrieveProducDetails();
			AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetrieveProductsFinised;
		} 
	}

	private static void OnRetrieveProductsFinised(BillingResult result) 
	{
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinised;
		
		if(result.isSuccess) 
		{
			_isInited = true;

			foreach(GooglePurchaseTemplate tpl in AndroidInAppPurchaseManager.instance.inventory.purchases) 
			{
				//AndroidMessage.Create(tpl.title, tpl.SKU);
				switch(tpl.SKU)
				{
				case "panda":
					SingletonSaveData.instance.pandaLocked = false;
					break;

				case "lugg":
					SingletonSaveData.instance.juggLocked = false;
					break;

				case "demon":
					SingletonSaveData.instance.demonLocked = false;
					break;
				}
			}
		} 
		else 
		{

		}
	}

	void PurchaseCharacter()
	{
		switch(CharacterGuiController.characterSelected)
		{
		case 1:
			AndroidInAppPurchaseManager.instance.purchase (PANDA);
			break;

		case 2:
			AndroidInAppPurchaseManager.instance.purchase (LUGG);
			break;

		case 3:
			AndroidInAppPurchaseManager.instance.purchase (SPEEDY);
			break;
		}

	}
}

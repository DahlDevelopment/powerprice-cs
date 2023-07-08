using ObjCRuntime;
using powerprice_cs_server;

namespace powerprice_cs;


public partial class ViewController : NSViewController {

	EntsoEBroker _broker;
	PowerPriceServer _server;


    protected ViewController (NativeHandle handle) : base (handle)
	{
		// This constructor is required if the view controller is loaded from a xib or a storyboard.
		// Do not put any initialization here, use ViewDidLoad instead
	}

	public override void ViewDidLoad ()
	{
		base.ViewDidLoad ();

		_broker = new();
		_server = new(_broker);
		
	}

	public override NSObject RepresentedObject {
		get => base.RepresentedObject;
		set {
			base.RepresentedObject = value;

			// Update the view, if already loaded.
		}
	}
}


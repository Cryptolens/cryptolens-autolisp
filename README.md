# cryptolens-autolisp

This repository contains a .NET wrapper around the C# functions that can be used in AutoLISP.

## Examples
### Initialize the library

The assembly can be loaded using `_netload` as shown below. Keep in mind the double backslashes in the path name.

```
(command "_netload" "D:\\autolisp-modules\\bin\\Debug\\autolisp-modules.dll")
```

### Operations

#### Check license

The `checklicensefile` method requires two parameters, the RSAPublicKey and the path to the license file. It will check that the file has not been changed by the user and `MaxNoOfMachines` is greater than 0, this method will also match check that the license file contains the current machine.

```
_$ (checklicensefile "<RSAKeyValue><Modulus>sGbvxwdlDbqFXOMlVUnAF5ew0t0WpPW7rFpI5jHQOFkht/326dvh7t74RYeMpjy357NljouhpTLA3a6idnn4j6c3jmPWBkjZndGsPL4Bqm+fwE48nKpGPjkj4q/yzT4tHXBTyvaBjA8bVoCTnu+LiC4XEaLZRThGzIn5KQXKCigg6tQRy0GXE13XYFVz/x1mjFbT9/7dS8p85n8BuwlY5JvuBIQkKhuCNFfrUxBWyu87CFnXWjIupCD2VO/GbxaCvzrRjLZjAngLCMtZbYBALksqGPgTUN7ZM24XbPWyLtKPaXF2i4XRR9u6eTj5BfnLbKAU5PIVfjIS+vNYYogteQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>" "D:\\activationfile.skm")
(1)
```

If the call is successful, `1` is returned and `-1` otherwise.

#### Check expiration date

The `getexpirationdate` method works the same way as `checklicensefile` and requires the same number of arguments. The only difference is that if it is successful, the expiration date (as a unix timestamp) will be returned and `-1` otherwise.

```
_$ (getexpirationdate "<RSAKeyValue><Modulus>sGbvxwdlDbqFXOMlVUnAF5ew0t0WpPW7rFpI5jHQOFkht/326dvh7t74RYeMpjy357NljouhpTLA3a6idnn4j6c3jmPWBkjZndGsPL4Bqm+fwE48nKpGPjkj4q/yzT4tHXBTyvaBjA8bVoCTnu+LiC4XEaLZRThGzIn5KQXKCigg6tQRy0GXE13XYFVz/x1mjFbT9/7dS8p85n8BuwlY5JvuBIQkKhuCNFfrUxBWyu87CFnXWjIupCD2VO/GbxaCvzrRjLZjAngLCMtZbYBALksqGPgTUN7ZM24XbPWyLtKPaXF2i4XRR9u6eTj5BfnLbKAU5PIVfjIS+vNYYogteQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>" "D:\\activationfile.skm")
(1591262045)
```

#### Get machine code

The `getmachinecode` returns the machine code (aka hardware id) of the current machine, so that you can activate it in the dashboard and then send the license file to your customer.

```
_$ (getmachinecode)
("8946477129A8F2A0865CB0BA4B2AC42EF41742E2DB4689B510818BFDFA1365DE")
```
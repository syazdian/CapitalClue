async function synchronizeDbWithCache(file) {
    window.sqlitedb = window.sqlitedb || {
        init: false,
        cache: await caches.open('Bit-Besql')
    };

    const db = window.sqlitedb;

    const backupPath = `/${file}`;
    const cachePath = `/data/cache/${file.substring(0, file.indexOf('_bak'))}`;

    if (!db.init) {
        db.init = true;

        const resp = await db.cache.match(cachePath);

        if (resp && resp.ok) {
            const res = await resp.arrayBuffer();

            if (res) {
                console.log(`Restoring ${res.byteLength} bytes.`);
                window.Module.FS.writeFile(backupPath, new Uint8Array(res));
                return 0;
            }
        }
        return -1;
    }

    if (window.Module.FS.analyzePath(backupPath).exists) {
        const waitFlush = new Promise((done, _) => {
            setTimeout(done, 10);
        });

        await waitFlush;

        const data = window.Module.FS.readFile(backupPath);

        const blob = new Blob([data], {
            type: 'application/octet-stream',
            ok: true,
            status: 200
        });

        const headers = new Headers({
            'content-length': blob.size
        });

        const response = new Response(blob, {
            headers
        });

        await db.cache.put(cachePath, response);

        window.Module.FS.unlink(backupPath);

        return 1;
    }
    return -1;
}

function GetJson(jsonString) {
    // create the editor
    const container = document.getElementById("jsoneditor")
    const options = {
        onEditable() {
            return { field: false, value: false };
        }
    };
    const editor = new JSONEditor(container, options)

    var data = JSON.parse(jsonString);

    // set json
    //const initialJson = {
    //    "masterId": "STP00022533", "version": 1, "status": "Activated", "pos": { "dateTime": "2023-04-05T15:33:00Z", "store": "0960", "register": "12", "transactionId": "419", "user": "0000188" }, "vendor": { "dateTime": "2023-05-17T03:27:47.2554722+00:00", "clientId": "GlobalSTORE", "dealerCode": "BG127", "transactionId": "STP00022533", "transactionType": "Sale", "eSigned": false }, "originalSale": null, "contacts": [{ "tags": ["Residential", "Primary"], "language": "EN", "company": null, "person": { "firstName": "Meric", "middleName": " ", "lastName": "Testbfo" }, "emails": [{ "tags": ["Primary"], "address": "waipong.lee@staples.ca" }], "phones": [{ "tags": ["Residential", "Primary"], "number": "9051357975", "extension": null }], "addresses": [{ "tags": ["Primary"], "street": ["162 MAGNOLIA AV"], "city": "SCARBOROUGH", "provinceCode": "ON", "province": "ON", "postalCode": "M1K3K8" }] }], "tax": { "totalDevicesTax": 0.0, "totalTaxFromBell": 0.00, "provinceCode": "NL", "exemptionAccountNumber": null, "federalExempted": false, "provincialExempted": false }, "downPayment": { "taxAmountHST": 0.0, "taxAmountGST": 0.0, "taxAmountPST": 0.0, "totalAmount": 240.00, "amount": 240.00 }, "tenders": [], "orders": [{ "index": 1, "productType": "Wireless", "orderNumber": "753270", "orderDate": "2023-05-05T17:59:46", "orderType": "Personal", "orderSubType": "New", "serviceType": "Postpaid", "accountType": "Consumer", "customerSubType": "SmallBusiness", "brandIndicatorType": "BELL", "soldBy": { "userId": "333ST", "employeeId": "1497574" }, "account": { "number": "545970782", "deposit": null }, "subscribers": [{ "id": 1, "saleType": "Activation", "phoneNumber": "4164277603", "item": { "serialNumber": { "value": "355616304881513", "format": "MEID" }, "code": "107524", "itemId": "3066277", "upcCode": null, "quantity": 1, "model": "X130062X", "descriptionEn": "Apple iPhone 13 256GB Midnight", "descriptionFr": "Apple iPhone 13 256 Go Minuit", "unitPrice": 1181.52, "upfrontDiscount": 0.0, "totalDiscount": 0.00, "netPrice": { "taxAmountHST": 0.0, "taxAmountGST": 0.0, "taxAmountPST": 0.0, "totalAmount": 1181.52, "amount": 1181.52 }, "installment": { "deferredTax": false, "deferredTaxAmount": 0.0, "term": 24, "amortizedTax": false, "taxAmount": 97.55, "totalAmount": 847.92, "amount": 750.37 }, "residualValue": { "taxAmount": 24.85, "totalAmount": 216.00, "amount": 191.15 }, "downPayment": { "taxAmountHST": 0.0, "taxAmountGST": 0.0, "taxAmountPST": 0.0, "totalAmount": 240.00, "amount": 240.00 } }, "deposit": null, "bundleDiscount": null, "promo": null, "sim": { "id": "89302610102030209266", "code": "106945", "itemId": "3063877", "unitPrice": 0.01, "description": "BELL SIM - KIOSK | Bell Sim kiosque", "descriptionEn": null, "descriptionFr": null }, "plan": { "description": "2-YR NAC Tier 2", "serviceTier": null, "promotionCode": null, "otherInfo": null, "commission": { "associateAmount": 42.5, "taxAmount": 17.55, "totalAmount": 152.55, "amount": 135.0 }, "adjustment": { "itemId": null, "notes": null, "associateAmount": 0.0, "taxAmount": 0.0, "totalAmount": 0.0, "amount": 0.0 }, "code": "USHRLOY", "itemId": "3063539", "unitPrice": 70.00, "term": 24, "descriptionEn": "Unlimited Share Loyalty", "descriptionFr": "Tél Int illimité Loyalty" }, "services": [{ "commission": { "associateAmount": 0.0, "taxAmount": 0.00, "totalAmount": 0.00, "amount": 0.0 }, "adjustment": { "itemId": null, "notes": null, "associateAmount": 0.0, "taxAmount": 0.0, "totalAmount": 0.0, "amount": 0.0 }, "code": "LOYAL_OP", "itemId": "", "unitPrice": 5.00, "quantity": null, "description": null, "descriptionEn": "Loyalty Indicator", "descriptionFr": "La Loyalty Indicator" }, { "commission": { "associateAmount": 0.0, "taxAmount": 0.00, "totalAmount": 0.00, "amount": 0.0 }, "adjustment": { "itemId": null, "notes": null, "associateAmount": 0.0, "taxAmount": 0.0, "totalAmount": 0.0, "amount": 0.0 }, "code": "DTLACC", "itemId": "", "unitPrice": 15.00, "quantity": null, "description": null, "descriptionEn": "Detailed account activity", "descriptionFr": "Activité détaillée du compte" }, { "commission": { "associateAmount": 0.0, "taxAmount": 0.00, "totalAmount": 0.00, "amount": 0.0 }, "adjustment": { "itemId": null, "notes": null, "associateAmount": 0.0, "taxAmount": 0.0, "totalAmount": 0.0, "amount": 0.0 }, "code": "CFACDPM", "itemId": "", "unitPrice": 10.00, "quantity": null, "description": null, "descriptionEn": "Call Display", "descriptionFr": "Afficheur" }], "return": null }] }, { "index": 2, "productType": "Internet", "orderNumber": "BC5G975V", "orderDate": "2023-05-05T13:48:13", "orderType": null, "orderSubType": null, "serviceType": null, "accountType": null, "customerSubType": null, "brandIndicatorType": null, "soldBy": { "userId": "333ST", "employeeId": "1497574" }, "account": null, "subscribers": [{ "id": 1, "saleType": "PreOrder", "phoneNumber": null, "item": null, "deposit": null, "bundleDiscount": null, "promo": null, "sim": null, "plan": { "description": "Home Internet", "serviceTier": "FIBE50TV", "promotionCode": "PMBIC6956", "otherInfo": "BIAPN0375|IONBPUNLAUG22", "commission": { "associateAmount": 20.0, "taxAmount": 0.00, "totalAmount": 0.00, "amount": 0.0 }, "adjustment": { "itemId": null, "notes": null, "associateAmount": 0.0, "taxAmount": 0.0, "totalAmount": 0.0, "amount": 0.0 }, "code": "ABIR0069", "itemId": "3063831", "unitPrice": null, "term": null, "descriptionEn": null, "descriptionFr": null }, "services": [{ "commission": { "associateAmount": 0.0, "taxAmount": 0.00, "totalAmount": 0.00, "amount": 0.0 }, "adjustment": { "itemId": null, "notes": null, "associateAmount": 0.0, "taxAmount": 0.0, "totalAmount": 0.0, "amount": 0.0 }, "code": "ABVMCGD01", "itemId": "", "unitPrice": 0.00, "quantity": 1, "description": null, "descriptionEn": null, "descriptionFr": null }], "return": null }] }, { "index": 3, "productType": "HomePhone", "orderNumber": "BC5G975V", "orderDate": "2023-05-17T03:27:49.291274+00:00", "orderType": null, "orderSubType": null, "serviceType": null, "accountType": null, "customerSubType": null, "brandIndicatorType": null, "soldBy": { "userId": null, "employeeId": "0000188" }, "account": null, "subscribers": null }]
    //}
    editor.set(data)

    // get json
    //const updatedJson = editor.get()
}

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}
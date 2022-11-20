import React, { useEffect } from "react";
import Quagga from "quagga";

const BarcodeScanner = () => {
    useEffect(() => {
        Quagga.init({
            inputStream: {
                name: "live",
                type: "LiveStream"
            },
            decoder: {
                readers: [
                    "ean_reader"
                ]
            },
            numOfWorkers: 2,
            locator: {
                patchSize: "medium",
                halfSample: true
            },
            frequency: 10
        }, function(err) {
            if (err) {
                console.log("oopsie!");
                console.log(err);
                return;
            }
            Quagga.onDetected((data) => {
                const style = 'background-color: darkblue; color: white; font-style: italic; border: 5px solid hotpink; font-size: 2em;'
                let idx = 0;
                let oddSum = 0;
                let evenSum = 0;
                for (let x of data.codeResult.code) {
                    if (idx === 12) break;
                    if (idx % 2 === 0)
                        oddSum += parseInt(x);
                    else
                        evenSum += parseInt(x);
                    idx++;
                }
                evenSum *= 3;
                let chk = (oddSum + evenSum) % 10;
                if (chk !== 0) chk = 10 - chk;
                console.log("expecting check digit " + chk);
                if (parseInt(data.codeResult.code[12]) === chk) {
                    console.log("%c" + data.codeResult.code, style);
                } else {
                    console.error("invalid code " + data.codeResult.code);
                }

                navigator.vibrate(100);
            });
            Quagga.start();
        });
        return function cleanup() {
            Quagga.stop();
        }
    });

    return (<div>
        <div id="interactive" className="viewport" />
        <span id="txt">not found</span>
    </div>);
};

export default BarcodeScanner;

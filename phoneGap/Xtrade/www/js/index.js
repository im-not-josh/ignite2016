/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
var app = {
    // Application Constructor
    initialize: function() {
        this.bindEvents();
    },
    // Bind Event Listeners
    //
    // Bind any events that are required on startup. Common events are:
    // 'load', 'deviceready', 'offline', and 'online'.
    bindEvents: function() {
        document.addEventListener('deviceready', this.onDeviceReady, false);
        document.addEventListener('pause', this.onPause, false);
        document.addEventListener('resume', this.onResume, false);
    },
    // deviceready Event Handler
    //
    // The scope of 'this' is the event. In order to call the 'receivedEvent'
    // function, we must explicitly call 'app.receivedEvent(...);'
    onDeviceReady: function() {
        app.fetchRates();
        app.receivedEvent('deviceready');
    },
    onResume: function() {
        app.receivedEvent('onresume');
    },
    onPause: function() {
        app.receivedEvent('onpause');
    },
    // Update DOM on a Received Event
    receivedEvent: function(id) {
        var parentElement = document.getElementById(id);
        var listeningElement = parentElement.querySelector('.listening');
        var receivedElement = parentElement.querySelector('.received');

        listeningElement.setAttribute('style', 'display:none;');
        receivedElement.setAttribute('style', 'display:block;');

        console.log('Received Event: ' + id);
    },
    fetchRates: function() {
        $.ajax({
            type: 'GET',
            url: 'https://api.asb.co.nz/public/v1/exchange-rates?' + encodeURIComponent('apikey') + '=' + encodeURIComponent('l7xx8573e82a108846e2a5d4bf7631d89e41'),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function (data) {

                var items[];
                $.each(data.value, function(i, item){
                    alert('Got stuff back: ' + item);
                
                    items.push('<li>' + item.currencyCode + '</li>');
                });
                
            },
            error: function (msg) {
                alert('Error: ' + msg.responseText);
            }
        });
    }
};

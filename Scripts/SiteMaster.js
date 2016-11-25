
window.onload = function () {
	var ele = document.getElementById("loginField");			// if this element exist, the login page is shown and therefore no need to logout
	var ele2 = document.getElementById("CreateUserButton");		//           -''-                (element on Register and ChangePassword)
	var browser=get_browser();

	if((browser.name != "Firefox")){
	if (confirm('To yield the best results, please use firefox (version 20 or higher). Do you want to download the latest version of firefox?')) {
		window.location.href = "https://download.mozilla.org/?product=firefox-48.0.2-SSL&os=win&lang=de";
	} else {
   
	}
	
	}else if((browser.version < 20)){
		
	if (confirm('You are using an old version of firefox, your browser needs to be updated. Do you want to download the latest version of firefox?')) {
		window.location.href = "https://download.mozilla.org/?product=firefox-48.0.2-SSL&os=win&lang=de";
	} else {
   
	}
	}
	
    if(!ele&&!ele2){ // dont show timeout and logout link on Login.aspx, ChangePassword.aspx and Register.aspx
		logoutTime = parseInt(document.getElementById("logoutTime").value);
        timer(logoutTime);
    }
	
	if(!are_cookies_enabled()){
		alert("Please enable cookies in your web browser to continue");
	}
}



// detect JavaScript version
// based on http://stackoverflow.com/questions/5916900/how-can-you-detect-the-version-of-a-browser
function get_browser(){
    var ua=navigator.userAgent,tem,M=ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || []; 
    if(/trident/i.test(M[1])){
        tem=/\brv[ :]+(\d+)/g.exec(ua) || []; 
        return {name:'IE',version:(tem[1]||'')};
        }   
    if(M[1]==='Chrome'){
        tem=ua.match(/\bOPR\/(\d+)/)
        if(tem!=null)   {return {name:'Opera', version:tem[1]};}
        }   
    M=M[2]? [M[1], M[2]]: [navigator.appName, navigator.appVersion, '-?'];
    if((tem=ua.match(/version\/(\d+)/i))!=null) {M.splice(1,1,tem[1]);}
    return {
      name: M[0],
      version: M[1]
    };
 }


// recursive timer (reset by mouse move on page)
function timer(minRemainig) {
	var minRemainigInitial = minRemainig;
	var indicator = document.getElementById("timeDisplay");	// get logout time from element
	
  window.addEventListener("mousemove", resetTimer, false);	// reset timer if cursor position has been changed 
  function resetTimer(){
	 minRemainig = minRemainigInitial;
	 
	 // update time until log out 
	 if(indicator.innerHTML != "Timeout: " + minRemainig + " min <a id=\"lnklogout\" href=\"javascript:__doPostBack('ctl00$lnklogout','')\" style=\"color:White;\">[Log Out]</a>"){
		 indicator.innerHTML = "Timeout: " + minRemainig + " min <a id=\"lnklogout\" href=\"javascript:__doPostBack('ctl00$lnklogout','')\" style=\"color:White;\">[Log Out]</a>";	
	 }
  }
  
  // set remaining time 
  var remaining = function () {
    if (minRemainig >= 0) {
		
		// color logout time red in the last five minutes
		if(minRemainig <= 5){
			indicator.innerHTML = "<span style=\"color:red;font-weight:bold\">Timeout: " + minRemainig + " min </span><a id=\"lnklogout\" href=\"javascript:__doPostBack('ctl00$lnklogout','')\" style=\"color:White;\">[Log Out]</a>";	
		}else{
			indicator.innerHTML = "Timeout: " + minRemainig + " min <a id=\"lnklogout\" href=\"javascript:__doPostBack('ctl00$lnklogout','')\" style=\"color:White;\">[Log Out]</a>";	
		}
	 
      minRemainig--;
    } else {
      return false;
    }
  };
 
  var logout = function () {
	document.getElementById("lnklogout").click();
  };
  remaining.Timer(60000, Infinity, logout); // 60000 ; 100	// recursition
}

// author: Mathias Schäfer; https://wiki.selfhtml.org/wiki/JavaScript/Anwendung_und_Praxis/komfortable_Timer-Funktion -------------------------------------
// 
Function.prototype.Timer = function (interval, calls, onend) {
  var count = 0;
  var payloadFunction = this;
  var startTime = new Date();
  var callbackFunction = function () {
    return payloadFunction(startTime, count);
  };
  var endFunction = function () {
    if (onend) {
      onend(startTime, count, calls);
    }
  };
  var timerFunction = function () {
    count++;
    if (count < calls && callbackFunction() != false) {
      window.setTimeout(timerFunction, interval);
    } else {
      endFunction();
    }
  };
  timerFunction();
};
//--------------------------------------------------------------------------------------------------------------------------------------------------------


// check if cookies are enabled in browser
//----- author: Sveinbjorn Thordarson   -----------http://sveinbjorn.org/cookiecheck ---------------------------------------------------------------------

function are_cookies_enabled()
{
    var cookieEnabled = (navigator.cookieEnabled) ? true : false;	// directly check if cookies are enabled

	
	// if cookieset is undefined try to set a test cookie and check again
    if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled)
    { 
        document.cookie="testcookie";
        cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
    }
    return (cookieEnabled);
}

//--------------------------------------------------------------------------------------------------------------------------------------------------------

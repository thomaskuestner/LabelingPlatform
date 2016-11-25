// k-Space Astronauts labeling platform
//    (c) 2016 under Apache 2.0 license 
//    Thomas Kuestner, Martin Schwartz, Philip Wolf
//    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information
//-------------------------------------------- global variables -----------------------------------------------------//

var indexgroup = 1;       // index of current group (current page)
var iNgroup = "-";          // total amount of images to be looped through groups
var Packages=0;           // image package
var helper = 0;
var imageList = new Array();	// array containing all images that are displayed
var linkedImages = new Array();	// array containing all images that where linked by the user			
var isFullScreen = false;		// true if fullscreen is active
var unlabelled = [-1,-1];		// array with position of unlabelled images [previous, next]
var progress = "0%";			// current labelling progress
var bindImages = false;			// true if images are linked 
var selectImages = false;		// true if 'ctrl' key is pressed down and therfore linking of images is possible	
var currentImage = 0;			// Id of the current selected image
var keyPressed = [];			// array to check pressed keys
var keys;						// instance with key values
var character;					// instance with unicode charaters
var maxLayer = 0;				// holds maximum amount of layers (image with the most amount of layers)
var userIsWriting = false;		// true if user is reporting an error to the admin
var fullyLoaded = false;

var conversionFactorY = 1;		// y conversion factor for entering and leaving fullscreen
var conversionFactorX = 1;		// x conversion factor for entering and leaving fullscreen

// stored values of display that are being reassigned after leaving the fullscreen mode
var display_Width = 1;			
var display_Height = 1			
var oldScreenWidth = 1;		
	
var currentImage_ID = 0;		// Id of the current selected image 	
var CurrentFullScreenImage = 0;	// Id of images which is currently displayed in fullscreen


var Reset = false;				// reset variable, forces postback if true and image package from server is not valid


//------------------------------------------------ Options -------------------------------------------------------------//
var MouseSensitivity = 1;			// sensitivity factor for mouse movement [only natural numbers]
var outlineMode = false;			// true = outline mode on; false = outline mode off
var smoothenImage = true;			// smoothen image [true: on; false: off]
var preLink = true;			 		// link all images at the beginning

//----------------------------------------------------------------------------------------------------------------------//



//###########################       ASSIGNMENT OF MOUSE KEYS, KEYBOARD KEYS	AND SYMBOLS       #####################################################################################################################################

// assignment of keyboard keys
function shortCuts(){
	
	// increase / decrease keys
	this.increase = keys.arrowRight;				this.decrease = keys.arrowLeft;
	
	// identifier keys
	this.brightness = keys.b;		
	this.contrast = keys.c;			
	this.zoom = keys.z;
	this.Select = keys.ctrl;
	this.move = keys.m;
	
	// !! please mind that the array sizes are statically defined (in the code) and must not be changed !!  //
	
	// shortcuts
	this.rotate = [keys.ctrl, keys.r];
	this.Reset = [keys.ctrl, keys.alt];
	this.fullscreen = [keys.ctrl, keys.enter];
	this.linkAll = [keys.ctrl, keys.l];
	
	// alternative keys assignment 
	this.nextLayer = [keys.arrowDown, keys.number2, keys.NUM2];
	this.previousLayer = [keys.arrowUp, keys.number1, keys.NUM1];
	this.nextPage = [keys.arrowRight, keys.NUM6, keys.number6];
	this.previousPage = [keys.arrowLeft, keys.NUM4, keys.number4];
}


// assignment of mouse keys
function mouseCode(){
	
	// standard mouse key values
	this.left = 1;			
	this.right = 2; 			
	this.middle = 4;
	
	/*******************************************************************************ASSIGN MOUSE KEYS HERE*********************************************************************************************/
	
	// mouse controls, asign here each feature to a certain mouse key
	this.brightness = this.middle;			this.zoom = this.right;			this.resetImg = this.left + this.right;				this.def = -1; // default value (no mouse key is being pressed down)
	this.contrast = this.middle;			this.move = this.left;			this.rotate = this.right + this.middle;				this.Select = this.left;
	
	/**************************************************************************************************************************************************************************************************/
	
}


// -------------------     object that contains all needed unicode characters and  relevant functions
// When adding an unicode character, the value needs to be added in the constructor. 
// The description needs to be assigned to the corresponding unicode character, in order to do so, please
// use the switch-case statement in the method "getUniCodeSymbol". (The description needs to match the one in the package)
//
// (It it also possible to add the value directly to the function "getUniCodeSymbol")
class uniCodeCharacter{
	
	// add unicode value here:
	constructor(){
	
	this.brightness = '\&#9704';							this.contrast = '\&#9683';						this.undef = '#';								
	this.motion = "&#x219d";                                this.zoom = '\&#128269';						this.arrowLeft = '\&#706';
	this.temperature = "T";                                 this.rotate = '\u21BA';							this.arrowRight = '\&#707';
	this.slice = '\&#x2263';                                this.screen = '\&#x239a';						this.expandScaleButton = '\u21E5';
	this.homogeneity = '\&#127780';                         this.edge_sharpness = '\&#10654';               this.SNR = '\&#128585';
	this.artefact_burden = "&#x219d"; 
	
	this.hideScaleButton = '\&#x2716';
		
	}
	
	
	// add description here and assign to corresponding unicode value
	getUniCodeSymbol(info){		// returns unicode character to be displayed in labelquickinfo
			var uniCodeChar;
			switch(info){
				case "brightness": uniCodeChar = character.brightness; break;
				case "motion": uniCodeChar = character.motion; break;
				case "temperature": uniCodeChar = character.temperature; break;
				case "artefact_burden": uniCodeChar = character.artefact_burden; break;
				case "homogeneity": uniCodeChar = character.homogeneity; break;
				case "edge_sharpness": uniCodeChar = character.edge_sharpness; break;
				case "SNR": uniCodeChar = character.SNR; break;
				
				default: uniCodeChar = character.undef;
			}
			return uniCodeChar;
		}

	
} 

// object that contains all necessary keyboard codes
function KeyCode(){
	
	this.arrowRight= 39;		this.arrowDown = 40;			this.NUM6 = 102;   				this.z = 90;     			this.f = 70;		
	this.arrowLeft = 37;        this.arrowUp = 38;	            this.number6 = 54;              this.r = 82;				this.alt = 18;	
	this.c = 67;                this.number2 = 50; 	            this.NUM4 = 100;                this.esc = 27;				this.l = 76;
	this.ctrl = 17;             this.NUM1 = 97;                 this.number4 = 52;              this.m = 77;				
	this.NUM2 = 98;             this.number1 = 49;              this.b = 66;                    this.enter = 13;
	
}

//################################################################################################################################################################################################################################

// load packages with images
function getPackage(){
		try{document.getElementById("idWrapperContainer").style.cursor ="wait";}catch(ex){}
 $.ajax({
        type: "POST",
        url: "ViewImage_.aspx/getListOfPackageImage",
        data: '{index:' + (indexgroup-1) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
		beforeSend: function(){loadingPage();updateButtons();},
		complete: function(){
				bindImages = false;
				switchLinked();
				delete linkedImages;
				linkedImages = new Array();
				if(preLink && (imageList.length > 1)){
					$("#CheckMerge").click();		
				}
				if((document.getElementById("disableTutorial").value != "") && (indexgroup == 1)){
					$('#btnTutorial').click();
				}
				fullyLoaded = true;
		},
        success: function (response) {
			Packages = response.d;
			try{
				Packages = JSON.parse(response.d);
				OnSuccess();
				unlabelled = Packages[0].getUnlabelled;
				progress = Packages[0].getPercentage;
				iNgroup = Packages[0].getNumOfPages;
				$("#btnProgress").attr('value', indexgroup + '/' + iNgroup + ' [' + progress + ']');
				delete Packages;
				updateButtons();			// update buttons
				}catch(err){
					eval(Packages);	// try to execute received code from server (c#)
					if(Reset){
						indexgroup = 1;
						__doPostBack('btnProgress','');
						Reset = false;
					}
				}
        },
        failure: function (response) {
			alert("Error loading packages:\n" + response.message);
        }
    });
}



// while window is loading
window.onload = function () {

	try{document.getElementById("idWrapperContainer").style.cursor ="wait";}catch(ex){}		// change cursor style to 'wait'
	updateButtons();
	loadingPage();
	
	// check if page is the login page, if not set timer for auto log out
	var ele = document.getElementById("loginField");
    if(!ele){
		logoutTime = parseInt(document.getElementById("logoutTime").value);
        timer(logoutTime);
    }
	

	// set event listener for mouse and keyboard functions
	var labellingWindow = document.querySelector("#idWrapperContainer");
	labellingWindow.addEventListener("DOMMouseScroll", changeSlice, false);
	labellingWindow.addEventListener("mousedown", clicked, false);
	labellingWindow.addEventListener("mouseup", unClick, false);
	labellingWindow.addEventListener("mousemove", adjustImage, false);
	document.getElementById("CheckMerge").addEventListener("click", allLinked, false);
	document.getElementById("btnEndTestcase").addEventListener("click", endTestcase, false);
	document.getElementById("popup3").addEventListener("click", closeTutorial, false);
	document.getElementById("popup2").addEventListener("click", closeHelp, false);
	document.getElementById("popup1").addEventListener("click", report, false);
	window.addEventListener("keydown", keyBoardAdjust, false);
	window.addEventListener("keyup", keyBoardUp, false);
	document.getElementById("idWrapperContainer").style = "Height: " + (screen.height - 410) + "px";
	// for full screen mode
	document.addEventListener("fullscreenchange", function () {fullScreenChange();}, false);
	document.addEventListener("mozfullscreenchange", function () {fullScreenChange();}, false);
	document.addEventListener("webkitfullscreenchange", function () {fullScreenChange();}, false);
	document.addEventListener("msfullscreenchange", function () {fullScreenChange();}, false);
	
	
	// create new instance of an object containing the keyboard values
	keys = new KeyCode();
	character = new uniCodeCharacter();
	mouse = new mouseCode();
	shortCuts = new shortCuts();
	
	

}

// checks if an image is selected and the type of the event is a key pressed down - if true set array entry at according position (representing the ascii value)
function keySequence(e){
	var key = e.which || e.keyCode;
	// key sequence adjust
	if(currentImage !== 0){	
	keyPressed[e.keyCode] = e.type == 'keydown';	
	}
}

//------------------------------------------------------------------------------------------------------------------------------------------------------------------//
// checks which key has been pressed and calls the arccording  function
// uses a boolean array ('keyPressed[]'): if a key is pressed, an entry (value 'true') is added to the array at the postion of the ascii character e.g.: 
// The user presses key 'c' which has an ascii value of '67', so 'true' is added to the array at position 67 ('keyPressed[67] = true'). -> keySequence(e)
// The following code evaluates the boolean array for single keys and short cuts. It is thereby necessary to check for shortcut first to 
// prevent the single key funtion and the short cut function from bein executed simultaneously.

function keyBoardAdjust(e){
	// function based on: http://stackoverflow.com/questions/5203407/javascript-multiple-keys-pressed-at-once
	
	// set array with pressed key
	keySequence(e);
	
	var key = e.which || e.keyCode;
	var multipleKeys = false;			// true if multiple keys are pressed
	var currentImageLinked = false;		// holds if multiple images has been linked 
	
if(!userIsWriting){						// prohibits keyBoardAdjust if user is writing (reporting an error to the admin)
		
	
	// check if image is linked
	if(currentImage !== 0){
		currentImageLinked = checkIfLinked(currentImage.getImageId());
	}
	
	var adjustValue = 0;
	
	// multiple keys  -> short cuts (brightness, contrast, orientation, reset, move, link all images, enter/leave fullscreen mode )
	// sets adjust value and mouseButton of the corresponding image
		
	if(keyPressed[shortCuts.brightness]){			// adjust brightness by holding the 'b' key and pressing the left(decrease) and right(increase) arrow keys
		if(keyPressed[shortCuts.increase]){
			multipleKeys = true;
			adjustValue = 1;
			
		}else if(keyPressed[shortCuts.decrease]){
			multipleKeys = true;
			adjustValue = -1;
		}
		
		// update changes
		if(currentImageLinked && !isFullScreen){
			for(var i = 0; i < linkedImages.length; i++){
				linkedImages[i].mouseButton = mouse.brightness;
				linkedImages[i].currentBrightness += adjustValue;
			}
		}else{
			currentImage.mouseButton = mouse.brightness;
			currentImage.currentBrightness += adjustValue;
		}
		
		e.preventDefault();
		
	}else if(keyPressed[shortCuts.contrast]){	// adjust contrast (0.1 steps) by holding the 'c' key and pressing the left(decrease) and right(increase) arrow keys
		
		if(keyPressed[shortCuts.increase]){
			multipleKeys = true;
			adjustValue = 0.1;
			
		}else if(keyPressed[shortCuts.decrease]){
			multipleKeys = true;
			adjustValue = -0.1
		}
		
		// update changes
		if(currentImageLinked && !isFullScreen){
			for(var i = 0; i < linkedImages.length; i++){
				linkedImages[i].mouseButton = mouse.contrast;
				linkedImages[i].currentContrast += adjustValue;
			}
		}else{
			currentImage.mouseButton = mouse.contrast;
			currentImage.currentContrast += adjustValue;
		}
		e.preventDefault();
		
	}else if (keyPressed[shortCuts.zoom]){	// adjust zoom (0.1 steps) by holding the 'z' key and pressing the left(zoom out) and right(zoom in) arrow keys
	
		if(keyPressed[shortCuts.increase]){
			multipleKeys = true;
			adjustValue = 0.1;
			
		}else if(keyPressed[shortCuts.decrease]){
			multipleKeys = true;
			adjustValue = -0.1;
		}
		
		// update changes
		if(currentImageLinked && !isFullScreen){
			for(var i = 0; i < linkedImages.length; i++){
				linkedImages[i].mouseButton = mouse.zoom;
				linkedImages[i].currentZoom += adjustValue;
			}
		}else{
			currentImage.mouseButton = mouse.zoom;
			currentImage.currentZoom += adjustValue;
		}
		e.preventDefault();
		
	}else if(keyPressed[shortCuts.rotate[0]]&&keyPressed[shortCuts.rotate[1]]){	// rotate image (by +90° (counterclockwise)) by using the shortcut 'ctrl' + 'r'
		multipleKeys = true;
		
		// update changes
		if(currentImageLinked  && !isFullScreen){
			for(var i = 0; i < linkedImages.length; i++){
				linkedImages[i].rotateImage();
			}
		}else{
			currentImage.rotateImage();
		}
		e.preventDefault();
		
	}else if(keyPressed[shortCuts.Reset[0]] && keyPressed[shortCuts.Reset[1]]){		// reset image adjustments by using the shortcut 'alt' + 'ctrl' 
		multipleKeys = true;
		
		// update changes
		if(currentImageLinked && !isFullScreen){
			for(var i = 0; i < linkedImages.length; i++){
				linkedImages[i].resetImage()
			}
		}else{
			currentImage.resetImage()
		}
		e.preventDefault();
		
	}else if(keyPressed[shortCuts.move]){		// move image by pressing down the 'm' key and using the arrow keys to alter the image position
	
		var adjustValue2 = 0;
		
		if(keyPressed[keys.arrowRight]){		// move right
			multipleKeys = true;
			adjustValue = 10;
		}else if(keyPressed[keys.arrowLeft]){	// move left
			multipleKeys = true;
			adjustValue = -10;
		}
		
		if(keyPressed[keys.arrowUp]){			// move up
			multipleKeys = true;
			adjustValue2 = -10;
		}else if(keyPressed[keys.arrowDown]){	// move down
			multipleKeys = true;
			adjustValue2 = 10;
		}
		
		// update changes
		if(currentImageLinked && !isFullScreen){
			for(var i = 0; i < linkedImages.length; i++){
				linkedImages[i].mouseButton = mouse.move;
				linkedImages[i].currentMarginTop += adjustValue2;
				linkedImages[i].currentMarginLeft += adjustValue;
			}
		}else{
			currentImage.mouseButton = mouse.move;
			currentImage.currentMarginTop += adjustValue2;
			currentImage.currentMarginLeft += adjustValue;
		}
		e.preventDefault();	
	}else if(keyPressed[shortCuts.linkAll[0]] && keyPressed[shortCuts.linkAll[1]]){			// link all images on the page ('ctrl' + 'l')
		multipleKeys = true;	
		globalAdjustStatus();
		e.preventDefault();		
	}else if(keyPressed[shortCuts.fullscreen[0]] && keyPressed[shortCuts.fullscreen[1]]){ 	// enter/leave fullscreen mode ('ctrl' + 'enter')
			e.target.id = currentImage.getImageId();
			toggleFullScreen(e);
	}
	
	
	// reset values of single image or linked images
	if(multipleKeys){
		if(currentImageLinked && !isFullScreen){
			for(var i = 0; i < linkedImages.length; i++){
				linkedImages[i].changeAttributes(e);
				linkedImages[i].mouseButton == mouse.def;
			}
		}else{
		currentImage.changeAttributes(e);
		currentImage.mouseButton == mouse.def;
		}
		e.preventDefault();
	}

	
	if((currentImage !== 0) && !multipleKeys){					// single key adjust (change layer, go to next/previous page, enable image selecting)

		if((shortCuts.nextLayer[0] == key)||(shortCuts.nextLayer[1] == key)||(shortCuts.nextLayer[2] == key)||(shortCuts.previousLayer[0] == key)||(shortCuts.previousLayer[1] == key)||(shortCuts.previousLayer[2] == key)){ // scroll layers
			// update changes
			if(currentImageLinked&&!isFullScreen){
				for(var i = 0; i < linkedImages.length; i++){
					linkedImages[i].doScroll(e);
				}
			}else{
			currentImage.doScroll(e);
			}
		}else if(((shortCuts.nextPage[0] == key)||(shortCuts.nextPage[1] == key)||(shortCuts.nextPage[2] == key))&&!(iNgroup == 1)&&!isFullScreen){	// go to next page
			setTimeout(function(){ $( "#btnForward" ).trigger( "click" ); }, 100); // timeout is needed to get the "keyup"-event and update the array (due to asynchronous function call)
		}else if(((shortCuts.previousPage[0] == key)||(shortCuts.previousPage[1] == key)||(shortCuts.previousPage[2] == key))&&!(iNgroup == 1)&&!isFullScreen){		// go to previous page
			setTimeout(function(){ $( "#btnBack" ).trigger( "click" ); }, 100); // timeout is needed to get the "keyup"-event and update the array (due to asynchronous function call)
		}else if((shortCuts.Select == key)&&(!isFullScreen)){			// enable image selecting
			selectImages = true;
		}
		
	}
}else{	
// esc sequence for when user is reporting an error
if(keys.esc == key){
	// $('#popup2Scroll').scrollTop(0);
	// $('#popup3Scroll').scrollTop(0);
	$('#popup1').hide();
	$('#popup2').hide();
	$('#popup3').hide();
	document.getElementById("message").value = "";	
	userIsWriting = false;

}
}

	//e.preventDefault();
	
}

// if a key is stopped being pressed down, disable image selecting and refresh array with image position
function keyBoardUp(e){
	if(!userIsWriting){
	keySequence(e);
	selectImages = false;
	}
}

// 
function untag(e){
		if(outlineMode && (e.target.className != "Display")||(e.target.className != "image")){
		
		var linked = false;
		try{
			for(var i = 0; (i < linkedImages.length); i++){
				if(linkedImages[i].getImageId() == currentImage.getImageId()){
					linked = true;
				}
			}
		}catch(ex){}
			
			if(linked){
				try{document.getElementById("display"+currentImage.getImageId()).style.outline = "5px solid #0000ff";}catch(ex){}
			}else{
				try{document.getElementById("display"+currentImage.getImageId()).style.outline = "";}catch(ex){}
			}
		}
}

//
function tag(e){
	
		if (outlineMode && (e.target !== e.currentTarget)) {
		if((e.target.className == "Display")||(e.target.className == "image")){
		currentImage = getImage(e);
		
		var linked = false;
		try{
			for(var i = 0; (i < linkedImages.length); i++){
				if(linkedImages[i].getImageId() == currentImage.getImageId()){
					linked = true;
				}
			}
		}catch(ex){}
			
			if(linked){
				try{document.getElementById("display"+currentImage.getImageId()).style.outline = "5px dashed #00ff00";}catch(ex){}
			}else{
				try{document.getElementById("display"+currentImage.getImageId()).style.outline = "5px dashed #ffff00";}catch(ex){}
			}
	
		}
	}
	
}
// when page is loaded get number if pages from server and update "tutorial button"
$(document).ready(function () {
		
		if(document.getElementById("disableTutorial").value != ""){
			document.getElementById("btnTutorial").src = "imgHelp/tutorial.png";
			}else{
			$('#btnTutorial').attr("disabled", true);
			document.getElementById("btnTutorial").style.cursor ="default";
			document.getElementById("btnTutorial").title="No tutorial available"
			}

	
	//getNGroup();
	getPackage();
	
	
	// add event listener to save current labels if the labelling page is left
	document.getElementById("bar").addEventListener("mousedown", Save, false);	
	

			

		
				
	
	
	
	
	
	
});

// get number of groups from server (equals number of pages)
function getNGroup(){
	
	    $.ajax({
        type: "POST",
        url: "ViewImage_.aspx/getNGroup",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            iNgroup = response.d;
			window.setTimeout(getPackage(),500);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
	
}

// reset value of mouse key, if lifted 
function unClick(e){
	if(((e.target.className=="Display")||(e.target.className=="image"))&&(!userIsWriting)){
		currentImage = getImage(e);
		currentImage.mouseButton = mouse.def;
		try{document.getElementById("display"+currentImage.getImageId()).style.cursor ="crosshair";}catch(ex){}
	}
}




// adjust image with mouse	
function adjustImage(e){
	if ((e.target !== e.currentTarget)&&(!userIsWriting)) {
		if((e.target.className == "Display")||(e.target.className == "image")){
			currentImage = getImage(e);
			tag(e);
			if(currentImage.mouseButton != mouse.def){
				currentImage.mouseButton = e.buttons;
				
				if(!bindImages){							// no images are linked, adjust single image
					currentImage.changeAttributes(e);
				}else{									
					var currentImageLinked = checkIfLinked(currentImage.getImageId());
					
					if(!currentImageLinked){				// adjust single image
						currentImage.changeAttributes(e);
					}else{									// adjust linked images
					if(currentImageLinked&&!isFullScreen){
						for(var i = 0; i < linkedImages.length; i++){
							linkedImages[i].mouseButton = e.buttons;
							linkedImages[i].changeAttributes(e);
						}
					}else{
						currentImage.changeAttributes(e);
					}
					}
				}
			}
			
		// disable adjust on labelling box
		}else if((e.target.className=="ui-slider-handle ui-state-default ui-corner-all")||(e.target.className=="ui-slider-handle ui-state-default ui-corner-all ui-state-hover")){	
			return;
		}else{
		}
    }else{
		untag(e);
		e.stopPropagation();				// disable all browser mouse functions  (only when hovering over image display)
	}
	return false;
}	



// checks which mouse button is clicked 
function clicked(e){
	if ((e.target !== e.currentTarget)&&(!userIsWriting)) {	 					// prohibit if mouse leaves the current display and if user is reporting an error
		
		if((e.target.className=="Display")||(e.target.className=="image")){		// allow funtion only for display an contained image
			currentImage = getImage(e);
			
			// check if 'ctrl' button on keyboard is pressed down
			if(selectImages){
				if(e.buttons == mouse.Select){ 		// images can only be linked individually by holding the 'ctlr' key an clicking on the image with the left mouse key
				bindImages = true;			// a image has been selected
				switchLinked();
				
				// check if image is already selected
				var imageAlreadySelected = false;
				var i = 0;
				
				while(i < linkedImages.length){
					if(linkedImages[i].getImageId() == currentImage.getImageId()){
						imageAlreadySelected = true;
						break;
					}
					i++;
				}

				if(!imageAlreadySelected){
					linkedImages.push(currentImage); 		// add image to the array containg all linked images 
					document.getElementById("display"+currentImage.getImageId()).style.outline = "5px solid #0000ff";	// highlight all images with a blue outline
				}
				else{										// if image is already selected it will be deselected
					document.getElementById("display"+linkedImages[i].getImageId()).style.outline = "";						// remove blue border
					linkedImages.splice(i, 1);				// remove image from the array containg all linked images 		
				}
				
				
				// reset checkbox 'link images'
				if(linkedImages.length == 0){
					bindImages = false;
					switchLinked();
				}

				}
			}else{
				// allocates the current mouse positon value to linked displays
				if(bindImages){
					var currentImageLinked = checkIfLinked(currentImage.getImageId());
		
					if(!currentImageLinked){

					}else{
						for(var i = 0; i < linkedImages.length; i++){
							linkedImages[i].mouseButton = e.buttons;
							linkedImages[i].OldX = e.pageX;
							linkedImages[i].OldY = e.pageY;
						}
					}
				}
			
			// stores the current mouse position globally
			currentImage.mouseButton = e.buttons;
			currentImage.OldX = e.pageX;
			currentImage.OldY = e.pageY;
			
			switch(currentImage.mouseButton){ // differentiate between the different options
				
				case mouse.left:{	// left mouse button
					try{document.getElementById("display"+currentImage.getImageId()).style.cursor ="grabbing";}catch(ex){}	// change style of mouse cursor
					break;
					}	
					
				case mouse.right:{	// right mouse button
					
					break;
					}
					
				case (mouse.left + mouse.right):{	// left and right mouse buttons pressed simultaniously -> reset image
					if(bindImages && currentImageLinked && !isFullScreen){					
						for(var i = 0; i < linkedImages.length; i++){
							linkedImages[i].resetImage();
							linkedImages[i].mouseButton = mouse.def;
						}
					}else{
						currentImage.resetImage();
						currentImage.mouseButton = mouse.def;
						}
					break;
					}
					
				case mouse.middle:{	// mouse wheel button
				try{document.getElementById("display" + currentImage.getImageId()).style.cursor ="copy";}catch(ex){}		// change style of mouse cursor
					e.preventDefault();
					
					break;
					}	
					
				case (mouse.left + mouse.middle):{	// left mouse button and mouse wheel button
					;
					break;
					}	
					
				case (mouse.middle + mouse.right):{	// right mouse button and mouse wheel button pressed simultaniously -> rotate image counterclockwise by 90°
				
					if(bindImages && currentImageLinked && !isFullScreen){
						for(var i = 0; i < linkedImages.length; i++){
							linkedImages[i].rotateImage();
							linkedImages[i].mouseButton = mouse.def;
						}
					}else{
						currentImage.rotateImage();
						currentImage.mouseButton = mouse.def;
					}
					
					break;
					}	
					
				default: e.preventDefault();
			}
			
		}
		}else if(e.target.className == "textIcon"){ 		// hide label if icon is pressed
			var Id = e.target.id;
			var lable = Id;
			Id = lable.replace("hideScale", "");
			for(var i = 0; i < imageList.length; i++){
		    if(imageList[i].getImageId() == Id){
			   	currentImage = imageList[i];
				break;
				}
			}
			currentImage.updateQuickInfo();
			$("#scaleBox"+Id).toggle();
			$("#expandScaleButton"+Id).toggle();
		}else if((e.target.className == "topright")){			// enter / exist fullscreen if icaon is pressed
			toggleFullScreen(e);
		}else if(e.target.className == "expandScaleButton"){	// show label if icon is pressed
			var Id = e.target.id;
			var lable = Id;
			Id = lable.replace("expandScaleButton", "");
			$("#scaleBox"+Id).toggle();
			$("#expandScaleButton"+Id).toggle();
		}																									
		else if((e.target.className == "ui-slider-handle ui-state-default ui-corner-all")||(e.target.className == "ui-slider-handle ui-state-default ui-corner-all ui-state-active ui-state-focus ui-state-hover")){	
			return;
		}else if(e.target.className == "arrow"){			// used for change image while in fullscreen mode (not feasible yet)
			// var direction = e.target.id;
			// direction = direction.replace(currentImage.getImageId(), "");
			// if(direction == "arrowLeft"){
				// CurrentFullScreenImage--;
				// alert("previous" + e.target.id);
			// }else if(direction == "arrowRight"){
				// CurrentFullScreenImage++;
				 // currentImage = imageList[CurrentFullScreenImage];
				// var elem = document.getElementById("display"+currentImage.getImageId())


				// toggleFullScreen(e);

			// //	window.setTimeout(toggleFullScreen(e),3000);
			// }
		}else{
			e.stopPropagation();
		}
    }else{
		
		

	}
	currentImage = 0;			// deselect image
    e.stopPropagation();	

	
}






// enter leave fullscreen (not possible in all browsers)	
function toggleFullScreen(e) {
		
		// get Id of image on which fullscreen mode has been called
		var Id = e.target.id;
		var lable = Id;
		Id = lable.replace("goFullScreenButton", "");
		
		// get current image 
		for(var i = 0; i < imageList.length; i++){
		   if(imageList[i].getImageId() == Id){
			  currentImage = imageList[i];
			  currentImage_ID = currentImage.getImageId();
			  CurrentFullScreenImage = i;
		   }
	    }
		
		// get element (in this case display) 
		var elem = document.getElementById("display"+Id);
		
		// store display dimensions if fullscreen is turned off 
		if(!isFullScreen){
			oldScreenWidth = currentImage.screenWidth;
			display_Width = currentImage.displayLength;
			display_Height = currentImage.displayHeight;
			conversionFactorY = screen.height / currentImage.displayHeight;
			conversionFactorX = screen.width / currentImage.displayLength;
			
			// display arrows for image navigation while in fullscreen
			if(CurrentFullScreenImage > 0){
				// $('#arrowLeft' + currentImage_ID).show();
			}
				if(CurrentFullScreenImage < imageList.length-1){
				// $('#arrowRight' + currentImage_ID).show();
			}
			
		}else{	// hide arrwos when not in fullscreen
			$('#arrowLeft' + currentImage_ID).hide();
			$('#arrowRight' + currentImage_ID).hide();
		}
		
	// if fullscreen mode is entered adjust display attributes	
	if (!document.mozFullScreen && !document.webkitFullScreen) { // enter fullscreen mode	
		currentImage.screenWidth = screen.width;
		currentImage.displayLength = screen.width;
		currentImage.displayHeight = screen.height;
		currentImage.currentMarginTop *= conversionFactorY;
		currentImage.currentMarginLeft *= conversionFactorX;
		currentImage.updateImage();
		
	// enter fullscreen (not all browsers are capable of displaying an element on fullscreen with this functions)
     if (elem.mozRequestFullScreen) {
        elem.mozRequestFullScreen();
     } else {
        elem.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
     }
    }else{	// exit fullscreen mode	
	// if fullscreen mode is left reset display attributes	
		currentImage.screenWidth = oldScreenWidth;
		currentImage.displayLength = display_Width;
		currentImage.displayHeight = display_Height;
		currentImage.currentMarginTop /= conversionFactorY;
		currentImage.updateImage();
		
		// exit fullscreen mode
		if (document.mozCancelFullScreen) {
			document.mozCancelFullScreen();
		} else {
			document.webkitCancelFullScreen();
		}
	}
	currentImage.updateImage();
	currentImage.updateQuickInfo();
}

	// function called when fullscreen mode is changed
	function fullScreenChange(){
	isFullScreen = !isFullScreen;	// set fullscreen variable (boolean) to current mode
	
			// get current image
		var currImage;
		for(var i = 0; i < imageList.length; i++){
		   if(imageList[i].getImageId() == currentImage_ID){
			   	currImage = imageList[i];
		   }
	   }
	
	if(!isFullScreen){	// if fullscreen mode is left
	// reset display attributes	(in case the user pressed 'ESC' to exit fullscreen mode ['ESC' can not be handled with an event listener while in fullscreen mode]}
		currImage.screenWidth = oldScreenWidth;
		currImage.displayLength = display_Width;
		currImage.displayHeight = display_Height;
		currImage.currentMarginTop /= conversionFactorY;
		currImage.currentMarginLeft /= conversionFactorX;
		document.getElementById("goFullScreenButton"+currImage._imageId).style.fontSize = "20px";
		}else{
		document.getElementById("goFullScreenButton"+currImage._imageId).style.fontSize = "40px";
		}
		currImage.updateImage();
		currImage.updateQuickInfo();
	}

	function changeSlice(e) {	// change the current layer
    if ((e.target !== e.currentTarget)&&(!userIsWriting)) { // disabled if user is reporting an error
		currentImage = getImage(e);
		if((e.target.className == "Display") || (e.target.className == "image")){
			var currentImageLinked = checkIfLinked(currentImage.getImageId());
			if(bindImages && currentImageLinked && !isFullScreen){
				for(var i = 0; i < linkedImages.length; i++){
					linkedImages[i].doScroll(e);
				}
			}else{
				currentImage.doScroll(e);
			}
			
			// prohibit scroll when cursor on symbol
		}else if((e.target.className=="textIcon")||(e.target.className=="field")||(e.target.className=="topright")||(e.target.className=="radioText")||(e.target.className=="discreteradio")||(e.target.className=="bottomleft")){
			e.preventDefault(); 
			e.stopPropagation();
		}
    }
    e.stopPropagation();
	}

		
	// display loading page while next next image package is loaded
	function loadingPage(){
		
		// delete images in container
		try{$('#idWrapperContainer').empty();}catch(ex){}	
		
		try{document.getElementById("idWrapperContainer").style.cursor ="wait";}catch(ex){}		// change cursor style to 'wait'
		
		// display loading *.gif
		try{var html ='<div style="position: relative;background:rgb(0,0,0); width:635px; height:300px; margin: 10px;"><img id="img_wait" style="position:absolute; left:295px; bottom: 127px; width: 46px; height: 46px; text-align:center;" alt="Loading..." oncontextmenu="return false;" ondragstart="return false;" src="/images/ajax-loader.gif"/><br/></div><br />';}catch(ex){}
		
		// apend waiting image
		try{$('#idWrapperContainer').append(html);}catch(ex){}	
	}			
	
	// called when user clicks 'link image' in toolbar -> all images are being linked or unlinked
	function globalAdjustStatus(){
		bindImages = !bindImages;
		// change state of 'link image'
		switchLinked();
		
		if(bindImages){	

			linkedImages = imageList.concat();	// copy content of array with all images into array with linked images (mind: elsewise ('linkedImages = imageList') reference is copied)
			maxLayer = 0;
			
			// get maximum value of layers from all images
			for(var i = 0; i < linkedImages.length; i++){
				document.getElementById("display"+linkedImages[i].getImageId()).style.outline = "5px solid #0000ff";	// highlight all images with a blue outline
				if(linkedImages[i].amountOfLayers() > maxLayer){
					maxLayer = linkedImages[i].amountOfLayers();
				}
			}
		}else{

				for(var i = 0; i < linkedImages.length; i++){
					document.getElementById("display"+linkedImages[i].getImageId()).style.outline = "";					// delete highlighting on every image
				}
				
				// clear obsolete array for garbage collection
				delete linkedImages;
				linkedImages = new Array();	
		}
		return false;
	}

	// next button was clicked -> save labels if altered; clear displays; increment page index; load image package of new page
	function next() {
		Save();
		$('#idWrapperContainer').empty();
		indexgroup++;
		$(this).attr("index", (indexgroup - 1));	// refresh displayed index
		getPackage();
		return false;
	}



	// previous button was clicked -> save labels if altered; clear displays; decrement page index; load image package of new page
	function previous() {
		Save();
		$('#idWrapperContainer').empty();
		indexgroup--;
		$(this).attr("index", (indexgroup - 1));	// refresh displayed index
		getPackage();
		return false;
	}

	// function to obtain the current image
	function getImage(e){
				try{	
				
			untag(e);
			
			// get image id
			var currImage;
			var Id = e.target.id;
			var lable = Id;
			if(Id.length < 8){
				Id = lable.replace("img", "");
			}else{
				Id = lable.replace("display", "");
			}
			
		// set current image 	
		for(var i = 0; i < imageList.length; i++){
			if(imageList[i].getImageId() == Id){
					currImage = imageList[i];
			}
		}

			return currImage;
			}catch(ex){
				return false;
				
			}

	}

	// update the button panel
	function updateButtons(){
	
			$(this).attr("index", (indexgroup-1));			// update page number
			updateGlobalReferences();						// update global reference image 
			
			if (indexgroup == 1) {							// first page, backward button is disabled
			$("#btnForward").attr("disabled", false);
			$("#btnForward").css("color", "black");
			$("#btnBack").attr("disabled", true);
			$("#btnBack").css("color", "gray");
		}
		else if (indexgroup == iNgroup) {					// last page, forward button is disable
			$("#btnForward").attr("disabled", true);
			$("#btnForward").css("color", "gray");
			$("#btnBack").attr("disabled", false);
			$("#btnBack").css("color", "black");
		}
		else {												// page between first and last page, forward and backward buttons are enabled
			$("#btnForward").attr("disabled", false);
			$("#btnForward").css("color", "black");
			$("#btnBack").attr("disabled", false);
			$("#btnBack").css("color", "black");
		}													
		if(iNgroup < 2){ 									// special case: only one file in testcase - forward and backward buttons are disabled
			$("#btnForward").attr("disabled", true);
			$("#btnForward").css("color", "gray");
			$("#btnBack").attr("disabled", true);
			$("#btnBack").css("color", "gray");
		}
		
		// update 'go to previous / next unlabelled' buttons (default value = -1)
		if(unlabelled[0] == -1){
			$("#btnPreviousUnlabelled").attr("disabled", true);
			$("#btnPreviousUnlabelled").css("color", "gray");
		}else{
			$("#btnPreviousUnlabelled").attr("disabled", false);
			$("#btnPreviousUnlabelled").css("color", "black");
		}
		
		if(unlabelled[1] == -1){
			$("#btnNextUnlabelled").attr("disabled", true);
			$("#btnNextUnlabelled").css("color", "gray");
		}else{
			$("#btnNextUnlabelled").attr("disabled", false);
			$("#btnNextUnlabelled").css("color", "black");
		}
		
		
	}	

	//------------------------------------------- BUTTON CONTROL -------------------------------------------//
	
	// go to previous unlabelled page
	$("#btnPreviousUnlabelled").on('click', function () {
	
		Save();					// save results
		removeImages();
		$('#idWrapperContainer').empty();
		indexgroup = unlabelled[0];	
		getPackage();	
		updateButtons();			// update buttons
			
		return false;
	});
	
	// go to next unlabelled page
	$("#btnNextUnlabelled").on('click', function () {
		
		Save();					// save results
		removeImages();
		$('#idWrapperContainer').empty();
		indexgroup = unlabelled[1];	
		getPackage();	
		updateButtons();			// update buttons
	
		return false;
	});


	$('#btnreset').on('click', function () {
		for(var i = 0; i < imageList.length; i++){
			imageList[i].resetImage();
		}
		return false;
	});

	$('#sendButton').on('click', function () {	// send error report to admin
		Save();						// save results
		userIsWriting = false;		// enable mouse and keyboard control
		$('#popup1').hide();		// close message box
		
		var messageText = document.getElementById("message").value;
		messageText = messageText.replace(/[^A-Za-z0-9_ ]/g,"");  		// remove harmful characters from message
		
		if(messageText != ""){ 						// only send a message if text has been typed in to the text box
			
		// get all information 
		var infoTestCase = "";
		var linked = "";
		if(bindImages){
			for(var i = 0; i < linkedImages.length; i++){
				linked += linkedImages[i].getImageId() + ";";
			}
		}
		infoTestCase = "current page: " + indexgroup + "; linked images: [" + linked + "]; progress: " + progress + "\n------------------------------------------------------------------------------------------------------------------------------------------------\n";
		for(var i = 0; i < imageList.length; i++){
			var labels = "";
			if(imageList[i]._isDiscrete){
				labels = "discrete: " + imageList[i].discreteValue;
			}else{
				for(var c = 0; c < imageList[i].listScaleCont.length; c++){
					labels += imageList[i].listScaleCont[c].ID_TypScaleContinuous + "[" + imageList[i].listScaleCont[c].DescriptionScaleContinuous + "]: " + imageList[i].listContValue[c].Lable + ";";
				}
			}
			infoTestCase += "ImageID: " + imageList[i].getImageId() + " CurrentImageValues: [S: " + imageList[i].currentSlice +"; B: "+imageList[i].currentBrightness + "; C: " + imageList[i].currentContrast + "; Z:" + imageList[i].currentZoom + "; R: " + imageList[i].currentRotation + ";]; Label:[" + labels + "]\n";
		}
		infoTestCase += "------------------------------------------------------------------------------------------------------------------------------------------------\nUser message:\n\n" + messageText;
	
		reportError_SendInfo(infoTestCase); // send message to c# code (email is sent via c#)
		}
	
		document.getElementById("message").value = "";
		return false;
	});
	
	
	// report error buttons
	$('#btnreport').on('click', function () {
		document.body.style.overflowY = "hidden";
		document.getElementById("popup1").style="height: " + $(document).height() + "px; visibility: visible;";
		userIsWriting = true;
		return false;
	});
	
	// show help page
	$('#btnHelp').on('click', function () {
		document.body.style.overflowY = "hidden";
		document.getElementById("popup2").style="height: " + $(document).height() + "px; visibility: visible;";
		userIsWriting = true;
		
		return false;
	});
	
	$('#closeHelp').on('click', function () {
		userIsWriting = false;
		//$('#popup2Scroll').scrollTop(0);
		$('#popup2').hide();
		
		return false;
	});
	
	$('#closeTutorial').on('click', function () {
		userIsWriting = false;
		//$('#popup3Scroll').scrollTop(0);
		$('#popup3').hide();
		
		return false;
	});
	
	$('#closeMessageBox').on('click', function () {
		userIsWriting = false;
		$('#popup1').hide();
		document.getElementById("message").value = "";
		return false;
	});
	
	// go to next next
	$("#btnForward").on('click', function () {
		
		Save();					// save results
		removeImages();
		indexgroup++;			// set index to next page
		getPackage();
		updateButtons();		// update buttons	
			
		return false;
	});
	
	// got to previous page
	$("#btnBack").on('click', function () {
		
		Save();				// save results
		removeImages();
		indexgroup--;       // set index to previous page
		getPackage();
		updateButtons();    // update buttons	
		
		return false;
	});
	
	
	// open tutorial link
	$("#btnTutorial").on('click', function () {
		var linkTutorial = document.getElementById("hddTutorial").value; 	// get link to tutorial page from hidden html element
		
		document.body.style.overflowY = "hidden";
		document.getElementById("popup3").style="height: " + $(document).height() + "px; visibility: visible;";
		userIsWriting = true;
		$('#TutorialPage').append(linkTutorial);
				
		return false;
	});
	
	//------------------------------------------------------------------------------------------------------//

	
	// put string containing all information in json format and send it to server (C# sends the email)
	function reportError_SendInfo(message){
		var msg = JSON.stringify(message); 
		
		$.ajax({
			type: "POST",
			url: "ViewImage_.aspx/reportError",
			data: '{message:' + msg + '}',
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			beforeSend: function(){
			},
			complete: function(){
			},
			success: function (response) {
	
			},
			failure: function (response) {
				alert("Error loading packages:\n"+response.message);
			}
		});
		
	}

	function report(ev){
		if("popup1" === ev.target.id){
			$('#closeMessageBox').click();
		}

		return false;
	}	
	
	function closeHelp(ev){
		if("popup2" === ev.target.id){
			$('#closeHelp').click();
		}

		return false;
	}
	
		function closeTutorial(ev){
		if("popup3" === ev.target.id){
			$('#closeTutorial').click();
		}

		return false;
	}
	
// start to create page
function OnSuccess() {
	$('#idWrapperContainer').empty();
	imageList = new Array();
	for(var i = 0; i < Packages.length; i++){
		imageList.push(new Image(Packages[i], Packages.length));
	}
	
	updateButtons();
	//ImgReference();
	try{document.getElementById("idWrapperContainer").style.cursor ="default";}catch(ex){}
	
}
	
	// check if current image is linked to others
	function checkIfLinked(currentImageID){
			var currentImageLinked = false; 				
		for(var i = 0; i < linkedImages.length; i++){
			if(linkedImages[i].getImageId() == currentImageID){
				currentImageLinked = true;
				break;
			}
		}
		return currentImageLinked;
	}
		
	// delete image references in order to be picked up by the garbage collection
	function removeImages(){
		for(var i = 0; i < imageList.length; i++){
			delete imageList[i];
		}
		delete imageList;
		
		for(var i = 0; i < linkedImages.length; i++){
			delete linkedImages[i];
		}
		delete linkedImages;
		currentImage = 0;
	}	
		
		

		
	
	// update global reference images
	function updateGlobalReferences(){
		$("input[id$='Button1'").click();
	}
		
	// load default page
	function home(){
		OrigWidth = 46;
		OrigHeight = 46;
		window.location.href = "../Default.aspx";
	}
	
	function allLinked(ev) {
		globalAdjustStatus();
		
		ev.preventDefault();
		return false;
	}
	
		// end current testcase and jump to default page
	function endTestcase(ev) {
		Save();					// save results
		$('#idWrapperContainer').empty();
		loadingPage();
		setTimeout(function(){ home(); }, 500);	// return to default page after specified time
		removeImages();
		delete shortCuts;
		delete mouse;
		delete character;
		delete keys;
		delete Packages;
		delete keyPressed;
		ev.preventDefault();
		
		return false;
	}
	
	function switchLinked(){
		var elemen = document.getElementById("CheckMerge");
			if(bindImages){
					elemen.src = "imgHelp/link.png";
					elemen.title="Unlink images"
				}else{
					elemen.src = "imgHelp/link_passiv.png";
					elemen.title="Link all images"
				}
	}
	
	function Save() {
		try{
			if(imageList[0].isDiscrete()){ // check if first image has a discrete scale (all images have the same scale type)
    			
    		var Listlablediscrete = [];
    		$("input[type=radio][class=discreteradio]").each(function () {
    			if ($(this).is(':checked')) {
    				var idGroup = $(this).attr("idgroupimage");
    				var value = $(this).val();
    					Listlablediscrete.push(
    						{ IdGroupImage: idGroup, Lable: value });
    			}
    		});
    
    		$.ajax({
    			type: "POST",
    			url: "ViewImage_.aspx/SaveLabelDiscrete",
    			data: "{Listlablediscrete:" + JSON.stringify(Listlablediscrete) + "}",
    			contentType: 'application/json; charset=utf-8',
    			dataType: "json",
    				
    			success: function (response) { 
    					},
    			failure: function (response) {
				alert(response.d);}
    			});
    			
    	}else if(imageList[0].isContinuous()){ // check if first image has a continuous scale (all images have the same scale type)
    			
    		var Listlablecontinuous = [];
    		$("input[type=text][class=scaleContinuous]").each(function () {
    			
    			var idScaleContinuous = $(this).attr("idScaleContinuos")
    			var idGroup = $(this).attr("idgroupimage");
    			var value = $(this).val();
    			var description = $(this).attr("DescriptionScaleContinuous");
    			var lable = value;
    			value = lable.replace(description + ": ", "");
    			var img = 0;
    			var imgId = ($(this).attr("id")).replace("valueSC_","");
    			imgId = imgId.substring(0, imgId.indexOf('_'));
    			for( var i = 0; i < imageList.length; i++){
    				if(imgId == imageList[i].getImageId()){
    					img = i;
    				}
    			}
    				// only values that have changed need to be saved
    				for(var k = 0; k < imageList[img].numOfConLabel; k++){
    					if(imageList[img].hasChangedCont[k] == idScaleContinuous){
    						Listlablecontinuous.push(
    						{ IdGroupImage: idGroup, Lable: value, IdScaleContinuous: idScaleContinuous });	
    					}
    				}
    				
    			
    		});
    		
    		
    		$.ajax({
    			type: 'POST',
    			url: "ViewImage_.aspx/SaveLabelContinuous",
    			data: "{Listlablecontinuous:" + JSON.stringify(Listlablecontinuous) + "}",
    			contentType: 'application/json; charset=utf-8',
    			dataType: 'json',
    			success: function (response) {
    						},
    			failure: function (response) {
    					alert(response.d);
    						}
    		});
    			
    	}
    
    		$("#output").empty();
    		$('#idImageContainerLeft').empty();
    		$('#idImageContainerRight').empty();
		}catch(ex){

		}
    	
  
    	return false;
    }
	
	

		

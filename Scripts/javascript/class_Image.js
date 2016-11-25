// k-Space Astronauts labeling platform
//    (c) 2016 under Apache 2.0 license 
//    Thomas Kuestner, Martin Schwartz, Philip Wolf
//    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information

class Image {

		// creates images with imformation provided by package (from class 'PackageImage' in C#)
		constructor(pack, numberOfImages){
			// image values not to be changed
			this._origWidth = pack.imageDimensions[0]; 		// width of the unzoomed picture in px
			this._origHeight = pack.imageDimensions[1];		// height of the unzoomed picture in px
			this._amountOfLayers = pack.imageDimensions[2];		// amount of image layers (formerly known as 'CurrentDepth') 
			this._amountOfImageFiles = pack.imagePaths.length;		// amount of images in directory (formerly known as 'CurrentDatasetLength' and 'package.Lenght')
			this._listOfImagePaths = pack.imagePaths;			// array with all paths (1 path for *.mhd, n paths for *.ima)
			this._imageId = pack.IdGroupImage;				// id of image
			this._numberOfImages = numberOfImages;			// number of images to be shown on page

			
			
			// label info  not to be changed
			this._isDiscrete = pack.DiskreteScale;			// true if image has a discrete scale
			this._isContinuous = pack.ContinuousScale;		// true if image has continuous scale
			this._discreteMin = pack.MinDiskreteScale; 		// minimum possible discrete label value
			this._dicreteMax = pack.MaxDiskreteScale; 		// maximum possible discrete label value
			this._isReference = pack.IsReference;			// true if image is reference (to be shown on left hand side, without scale)
			this._isRefGlobal = pack.ReferenceGlobal;			// true if reference is global
			this._worstDiscrete = pack.PathWorseGlobalDiscrete;		// path to reference image 'worst example'
			this._bestDiscrete = pack.PathBestGlobalDiscrete;		// path to reference image 'best example' 
			this._caseType = pack.Type; 						// type of test case
			

			// adjustable image values
			this.currentWidth = pack.imageDimensions[0]; 	// width of the picture in px
			this.currentHeight = pack.imageDimensions[1];	// height of the picture in px
			this.currentSlice = 1; 							// current shown slice (formerly known as 'CurrentID')
			this.currentRotation = 0; 						// current orientation of the image {0°, 90°, 180°, 270°}
			this.currentBrightness = 0; 					// current brightness value [-100, 100]
			this.currentContrast = 1; 						// current contrast value [0, 10]
			this.currentZoom = 1; 							// current zoom value [0.1, 10]    
			this.currentMarginLeft = 0;						// current image x-offset 
			this.currentMarginTop =  0;						// current image y-offset
			this.displayLength = '300';						// minimum length of display (in px)
			this.displayHeight = '300';						// minimum height of display (in px)
			this.screenWidth = document.getElementById("idWrapperContainer").clientWidth;	// store sceen width
			
			// label values
			this.listScaleCont = pack.TypeScaleContinuous; 		// array with label info (inherited from class 'TypScaleContinuous' in C#)
			this.discreteValue = pack.LableDiscrete;			// discrete label value (int)
			this.listContValue = pack.LableContinuous;			// array with label information and values (decimal)
			this.numOfConLabel = pack.LableContinuous.length; 		// amount of continuous labels
			this.hasChangedCont = new Array(this.numOfConLabel);	// array with information about change(if label has changed and therefore needs to be included in the saving process)
			this.alreadyLabelled = false;							// boolean: is image already completely labelled?
			this.labelQuickInfo = "";								// values of labels for when scales are collapsed
			
			//mouse control
			this.mouseButton = mouse.def;					// holds which mouse button has been clicked [Values: (Left: 1; Middle: 4; Right: 2)], capable of multiple simultaneous clicks (values add up)
			this.OldX = 0;									// previous x position of mouse cursor
			this.OldY = 0;									// previous y position of mouse cursor
			this.direction = "";							// Used for fullscreen navigation
			
			// function calls that create image
			this.createDisplay();
					
		}
		
		// accessor methods to constant values
		 originalWidth(){return this._origWidth;}
		 originalHeight(){return this._origHeight;}
		 amountOfLayers(){return this._amountOfLayers;}
		 amountOfImageFiles(){return this._amountOfImageFiles;}
		 imageId(){return this._imageId;}
		 isDiscrete(){return this._isDiscrete;}
		 isContinuous(){return this._isContinuous;}
		 discreteMin(){return this._discreteMin;}
		 dicreteMax(){return this._dicreteMax;}
		 isReference(){return this._isReference;}
		 isRefGlobal(){return this._isRefGlobal;}
		 worstDiscrete(){return this._worstDiscrete;}
		 bestDiscrete(){return this._bestDiscrete;}
		 caseType(){return this._caseType;}
		 getImageId(){return this._imageId;}

		
		
	// create single display	
	createDisplay(){			

		// determine display size based on number of displays per page and screen width
		
		// calculate image width
		switch(this._numberOfImages){
			case 1:{
				this.displayLength = this.screenWidth/2;
				break;
			}
			case 2:{
				this.displayLength = Math.round((this.screenWidth-(3)*25)/2);	
				break;
				}
			default:{
				if(this._numberOfImages%2 == 0){
					this.displayLength = Math.round((this.screenWidth-(3)*25)/2);
				}else{
					this.displayLength = Math.round((this.screenWidth-(5)*25)/3);
				}
			}
		}
		
		// calculate image height
		if(this._numberOfImages < 4){
			this.displayHeight = screen.height-600; // adjust offset value to screen resolution
		}
		

		
		

			// concatenate html sting
			var html = '<div class="Display" id="display' + this._imageId + '" style=" width:'+this.displayLength+'px; height:'+this.displayHeight+'px;';
			
			html+='">';
			// html += '<div id="arrowLeft' + this._imageId + '" class="arrow"/>';
			// html += '<div id="arrowRight' + this._imageId + '" style="right: 0px;" class="arrow"/>';
			html += '<img style="width:' + this.currentWidth + 'px; height:' + this.currentHeight + 'px; margin-top:' + (-(this._origHeight/2)) + 'px; margin-left:' + (-(this.displayLength)) + 'px;" id="img' + this._imageId + '" alt="error occured in image rendering!" oncontextmenu="return false;" ondragstart="return false;" class="image">';
			html += '<div id="infoText' + this._imageId + '" class="bottomleft"/>';
			html += '<div id="goFullScreenButton' + this._imageId + '" class="topright"/>';
			
			// add scale or mark as reference
			if ((this._isDiscrete) && (!this._isReference)){
				html += this.createScaleDiskrete();
			}
			else if ((!this._isDiscrete) && (!this._isReference)){
				html += this.createScaleContinuous();
			}
			if(this._isReference){
				html += '<fieldset style="width:1px; z-index: 3; position: absolute; top: -9px; left: -3px;" class="field"><span id="RefInfo" style="top: -5px; font-size: 20px; font-weight: bold; cursor: text; color:#ffff00">REFERENCE</span></fieldset>';
			}
			html += '</div>';
			
			$('#idWrapperContainer').append(html); // load html string
			
			
			// set already labelled labels
			if(this._isContinuous){
				for(var i = 0; i < this.hasChangedCont.length; i++){
					if(this.listContValue[i].Lable != -1){
						this.alreadyLabelled = true;
					}else{
						this.alreadyLabelled = false;
						break;
					}
				}
			}else if(this._isDiscrete){
				if(this.discreteValue != 0){
					this.alreadyLabelled = true;
				}else{
					this.alreadyLabelled = false;
				}
			}
			
			// display value of labelled labels in quick info (only if label box is collapsed)
			if(this.alreadyLabelled){
				document.getElementById("quickInfo" + this._imageId).innerHTML = this.labelQuickInfo;
				$('#scaleBox' + this._imageId).hide();
			}else{
				$('#expandScaleButton' + this._imageId).hide();
			}			
			

			this.manageScaleContinuous();	// update scales to current values
			this.updateImage();				// update image and info 
			
		
			// hide arrows (only needed in fullscreen mode)
			// $('#arrowLeft' + this._imageId).hide();
			// $('#arrowRight' + this._imageId).hide();
			
			// set symbol and size of arrows (only needed in fullscreen mode)
			// document.getElementById("arrowLeft"+this._imageId).innerHTML = character.arrowLeft;	
			// document.getElementById("arrowRight"+this._imageId).innerHTML = character.arrowRight;	
			// document.getElementById("arrowLeft"+ this._imageId).style.marginTop = screen.height/2 + 'px';
			// document.getElementById("arrowRight"+ this._imageId).style.marginTop = screen.height/2 + 'px';
	}
	
	
	// create discrete scale 
	createScaleDiskrete() {
			var html = '<fieldset id="showScaleBox' + this._imageId + '" style="width:1px; z-index: 3; position: absolute; top: 0px; left: 0px;" class="field"><span id="expandScaleButton' + this._imageId + '" class="expandScaleButton" style="font-size: 40px;">' + character.expandScaleButton + '<span style="overflow-x: visible; font-weight: bold; font-size: 16px; cursor: text; color:#44FA2F; vertical-align: 8px;" id="quickInfo' + this._imageId + '">' + this.labelQuickInfo + '</span></span></fieldset>';
			html += '<fieldset id="scaleBox' + this._imageId + '" style= "width:335px; z-index: 3; position: absolute; top: 5px; left: 5px; border:1px solid #ffff00; border-radius: 8px;" class="field"><span id="hideScale' + this._imageId + '" class="textIcon" style="top:-5px; right: 4px;">' + character.hideScaleButton + '</span>';
			
			// set checkboxes
			for (var i = this._discreteMin; i <= this._dicreteMax; i++) {
				
				if (i == this.discreteValue){
				html += ("<label class='radioText' style='font-weight: bold; color:#44FA2F;'><input type='radio' idgroupimage='" + this._imageId + "' id='myRadio" + i + "' name='discrete_" + this._imageId + "' value='" + i + "' class='discreteradio' checked >" + i+"</span></label>");
				this.labelQuickInfo = i;
				this.alreadyLabelled = true;
				}
				else{
				html += ("<label class='radioText'><input type='radio' idgroupimage='" + this._imageId + "' id='myRadio" + i + "' name='discrete_" + this._imageId + "' value='" + i + "' class='discreteradio'>" + i+"</span></label>");
				}
				
			}
			
			// already labelled
			if (this.discreteValue > 0) {		
			html += ("</div>");
			}	
			html += '</fieldset>';
	
			return html;
	}	
		
	// create continuous scale 	
	createScaleContinuous() {
		
		
			var html = '<fieldset id="showScaleBox' + this._imageId + '" style="width:1px; z-index: 3; position: absolute; top: 0px; left: 0px;" class="field"><span id="expandScaleButton' + this._imageId + '" class="expandScaleButton" style="font-size: 40px;">\u21E5<span style="font-size: 12px; cursor: text; color:#44FA2F; vertical-align: 8px;" id="quickInfo' + this._imageId + '">' + this.labelQuickInfo + '</span></span>';
			html+= '</fieldset><fieldset id="scaleBox' + this._imageId + '" style="width:25%; z-index: 3; position: absolute; top: 0px; left: 0px;" class="field"><span id="hideScale' + this._imageId + '" class="textIcon">\&#x2716</span><div id="eq_' + this._imageId + '" class="slider">';
			
			for(var list = 0; list < this.listScaleCont.length; list++){
				for(var scale = 0; scale < this.listContValue.length; scale++){
					if ((this.listContValue[scale].IdScaleContinuous == this.listScaleCont[list].ID_TypScaleContinuous) && (this.listContValue[scale].IdGroupImage == this._imageId)) {
					
						var space = '';
						while((this.listScaleCont[list].DescriptionScaleContinuous + space).length < 10){
							space += ' ';
						}
						if (this.listContValue[scale].Lable != -1) { // already labelled
							html += '<input type="text" idScaleContinuos="' + this.listScaleCont[list].ID_TypScaleContinuous + '" id="valueSC_' + this._imageId + "_" + this.listScaleCont[list].ID_TypScaleContinuous + '" value="' + this.listScaleCont[list].DescriptionScaleContinuous + ': '+space+'\t' + this.listContValue[scale].Lable + '" idgroupimage="' + this._imageId + '" DescriptionScaleContinuous="' + this.listScaleCont[list].DescriptionScaleContinuous + '" readonly style="border:0; color:#44FA2F; font-weight:bold;" class="scaleContinuous">';
							html += '<span nameinput="valueSC_' + this._imageId + '_' + this.listScaleCont[list].ID_TypScaleContinuous + '"   nameScale="' + this.listScaleCont[list].DescriptionScaleContinuous + '"></span>';
							this.labelQuickInfo += character.getUniCodeSymbol(this.listScaleCont[list].DescriptionScaleContinuous) + ":" + this.listContValue[list].Lable + "|";
						}
						else { // not yet labelled
							html += '<input type="text" idScaleContinuos="' + this.listScaleCont[list].ID_TypScaleContinuous + '" id="valueSC_' + this._imageId + "_" + this.listScaleCont[list].ID_TypScaleContinuous + '" value="' + this.listScaleCont[list].DescriptionScaleContinuous + ': '+space+'\t0" idgroupimage="' + this._imageId + '" DescriptionScaleContinuous="' + this.listScaleCont[list].DescriptionScaleContinuous + '" readonly style="border:0; color:#ff0000; font-weight:bold;" class="scaleContinuous">';
							html += '<span nameinput="valueSC_' + this._imageId + '_' + this.listScaleCont[list].ID_TypScaleContinuous + '"   nameScale="' + this.listScaleCont[list].DescriptionScaleContinuous + '"></span>';
						}
					}
				}

				html += '<br/>';
			}
			html += '</div></fieldset>';
			
			return html;
	}
		

	// manage scale continuous	
	manageScaleContinuous() {
		
		
		
			var zaehl = 0;
			var hasChangedIndex = 0;
			var idSlider = "#eq_" + this._imageId + " > span";
			
			var hasChanged = this.hasChangedCont;
			var listContValue = this.listContValue;
			var imageId = this._imageId;
												
			$(idSlider).each(function () {
	
				var IdScale = $(this).attr("nameinput");
				var cutter = "valueSC_" + imageId + "_";
				var lable = IdScale;
				IdScale = lable.replace(cutter, "");		
				var wertSchieber = 0;
				if(listContValue[zaehl].Lable!=-1){
					wertSchieber=listContValue[zaehl++].Lable;
				} 

				$(this).empty().slider({ 
					value: wertSchieber*100, 
					min: 0, 
					max: 100, 
					range: "min", 
					animate: true, 
					orientation: "horizontal", 
					start: function( event, ui ) {
						var alreadyChanged = false;
						for(var i = 0; i < hasChanged.length; i++){
							if(hasChanged[i] == IdScale){
								alreadyChanged = true;
							}
						}
						
						if(!alreadyChanged){
							hasChanged[hasChangedIndex++]=IdScale;
						}
					} ,
					slide: function (event, ui) {
						var space = "";
						var value = $(this).attr("nameinput");
						var nameScale = $(this).attr("nameScale");
						while((nameScale+space).length<10){
							space += " ";
						}
						$("#" + value).val(nameScale + ": "+space+"\t" + ui.value / 100);
					}
				});	
		
			});
			
	}		
	
	// update image values
	updateImage() {	
	
			this.updateImageInfo();
	
			var sBrightness = "&tXt9X3=" + this.currentBrightness;	
			sBrightness = sBrightness.replace('.', ',');
			var sContrast = "&XwjRGm=" + this.currentContrast;
			sContrast = sContrast.replace('.', ',');
	
			// not all browser can handle document.getElementsByClass.. workaround:
			var allElements = document.getElementsByTagName("*");
			for (var i = 0; i < allElements.length; i++) {
				if (allElements[i].className == "image") {
					if(allElements[i].id == "img"+this._imageId){
				
						var iI = 0;									// current position in array filled with paths
						var currentLayer = this.currentSlice; 		// id of current layer
					
						if(this._amountOfImageFiles > 1){						// for DICOM files
							iI = this.currentSlice - 1;							// shift iI (array with paths starts at 0) 
								if(this._amountOfImageFiles - 1 <= iI){ 		// last entry of array has been reached
									currentLayer = this._amountOfImageFiles;	// last layer, stop increasing layer index
								}							
						}

						allElements[i].src = "ShowImage.ashx?NqC3ke=" + (currentLayer) + sBrightness + sContrast + "&Hsfke2=" + this.currentRotation + "&yAR8st=" + this._amountOfImageFiles + "&WkYTCe=" + this._listOfImagePaths[iI] + "";
						allElements[i].style.width = this.currentWidth + "px";
						allElements[i].style.height = this.currentHeight + "px";
						allElements[i].style.marginTop =   - (this.currentHeight / 2) + this.currentMarginTop + "px";
						allElements[i].style.marginLeft =  - (this.displayLength) + this.currentMarginLeft + "px";
						
					}
				}
			}
			
			
			
	}
	
	
	
	// image info displayed in the button left corner
	updateImageInfo(){
		
		var slc = character.slice;
		var brt = character.brightness;
		var cntr = character.contrast;
		var zm = character.zoom;
		var rot = character.rotate;
		var screen = character.screen;

		var buttonFull = document.getElementById("goFullScreenButton"+this._imageId);
		buttonFull.innerHTML = screen; // set unicode character
		
		if(isFullScreen){
			document.getElementById("infoText"+this._imageId).style.fontSize = "26px";
		}else{
			document.getElementById("infoText"+this._imageId).style.fontSize = "initial"; // (13px)
		}
		
		document.getElementById("infoText"+this._imageId).innerHTML = slc + ": " + this.currentSlice + "/" + this._amountOfLayers + " | " + brt + ": " + this.currentBrightness.toFixed(0) + " | " + cntr + ": " + this.currentContrast.toFixed(1) + " | " + zm + ": " + this.currentZoom.toFixed(1) + " | " + rot + ": " + this.currentRotation + "°";
		

		
	}
		 
	// reset image parameter
	resetImage() {						// resets image parameters to initial value and updates changes
	
		this.currentWidth = this._origWidth;
		this.currentHeight = this._origHeight;
		this.currentBrightness = 0;
		this.currentContrast = 1;
		this.currentZoom = 1;
		this.currentRotation = 0;
		this.currentSlice = 1;
		this.currentMarginLeft = 0;
		this.currentMarginTop = 0;
		if(fullyLoaded){
			document.getElementById("img"+this._imageId).style.filter = "blur(0px)";
		}
		

		this.updateImage();
		
	}	 
		
	// rotates image by + 90° (counterclockwise)
	rotateImage() {		
		
		this.currentRotation = this.currentRotation + 90;
        if (this.currentRotation > 270){
			this.currentRotation = 0;
		}
        var tmp = this.currentWidth;
        this.currentWidth = this.currentHeight;
        this.currentHeight = tmp;

        this.updateImage();
		
        return false;
    }
	
	// only shown if label box is collapsed
	updateQuickInfo() {
		this.labelQuickInfo = "";
		var scaleInfo = this.listScaleCont;
		var that = this;
		if(this._isContinuous){
			
		// get label values if changed
		$("input[type=text][class=scaleContinuous][idgroupimage=" + that._imageId + "]").each(function () {
				var value = $(this).val();
				var description = $(this).attr("DescriptionScaleContinuous");
				var label = value;
				var id = -1;
				var LabelID = $(this).attr("id").replace("valueSC_" + that._imageId + "_", "")
				var changed = false;
				
				
				for(var i = 0; i < that.hasChangedCont.length; i++){
					if(LabelID == that.hasChangedCont[i]){
						changed = true;
						break;
					}
				}
				
				// Add symbol and value to labelQuickInfo
				value = label.replace(description, character.getUniCodeSymbol($(this).attr("DescriptionScaleContinuous")));
				for(var c = 0; c < that.listContValue.length; c++){
					if(that.listScaleCont[c].DescriptionScaleContinuous == description){
						id = c;
						break;
					}
				}
					if((that.listContValue[id].Lable > -1)||(changed)){
						label = '<span style="color:#44FA2F">' + value.replace(/\s+/g, '') + '</span>';
					}else{
						label = value.replace(/\s+/g, '');
						var temp = label.split(":");
						label = '<span style="color:#ff0000">' + temp[0] + ":-" + '</span>';
					}
				
				that.labelQuickInfo += label +"|"
				
		});		
		}else if(this._isDiscrete){	
			$("input[type=radio][class=discreteradio][idgroupimage=" + that._imageId + "]").each(function () {
					if ($(this).is(':checked')) {
						that.labelQuickInfo = '<span style="color:#44FA2F">' + $(this).val() + '</span>';
						return false;
					}else{
						that.labelQuickInfo = '<span style="color:#ff0000">' + "-" + '</span>';
					}			
			});
		}
		
		
		document.getElementById("quickInfo" + this._imageId).innerHTML = this.labelQuickInfo;
		
		if(isFullScreen){
			document.getElementById("quickInfo"+this._imageId).style.fontSize = "26px";
		}else{
			document.getElementById("quickInfo"+this._imageId).style.fontSize = "16px";
		}
		
		
	
	}
		 
	// event bound functions
    doScroll(e) {							// change image ID by mousewheel scrolling
        e.preventDefault(); 								// prevents browser window from being scrolled down (deactivates scrolling)
        e.returnValue = false;								// set return value (otherwise undefined)
		var delta = 0;
		var trigger = e.which || e.keyCode;
		
		// console.log(trigger);
		
		switch (trigger){
		case 1: {delta = Math.sign(e.detail); break;}		// mouse wheel: e.detail={-3 for down scroll; +3 for up scroll} -> sets values to -1/1
		case (shortCuts.nextLayer[0]): {delta = 1; break;}
		case (shortCuts.nextLayer[1]): {delta = 1; break;}
		case (shortCuts.nextLayer[2]): {delta = 1; break;}
		case (shortCuts.previousLayer[0]): {delta = -1; break;}
		case (shortCuts.previousLayer[1]): {delta = -1; break;}
		case (shortCuts.previousLayer[2]): {delta = -1; break;}
		default: delta = 0;
		}
		
			this.currentSlice += delta;		// change slice

		if(this.currentSlice > this._amountOfLayers){
			this.currentSlice = this._amountOfLayers;
		}
        
		
		
        if (this.currentSlice < 1) {						// prevents from scrolling below 1
            this.currentSlice = 1;							// set to first slice
			return false;											// prevents from loading the same image again
        }
		
		// if global, the same slices of all images are alway shown together if existing (e.g.: slice 1 of image 1 and slice 1 of image 2 are shown together)
		if(!bindImages){
			if (this.currentSlice > this._amountOfLayers) {		// prevents from scrolling above max slice
				this.currentSlice = this._amountOfLayers;		// set to last slice
				return;											// prevents from loading the same image again
			}else{
				this.updateImage();								// load new image layer
			}
		}else{
			if (!(this.currentSlice > this._amountOfLayers)) {
				this.updateImage();								// load new image layer
			}else{
				if(this._amountOfLayers == maxLayer){
					this.currentSlice = this._amountOfLayers;
				}else if(this.currentSlice > maxLayer){
					//this.currentSlice = maxLayer;
				}
			}
		}
		return false;
    }

	// change image attributes (brightness, contrast, orientation, zoom)
	changeAttributes(e){
		
		// get relative mouse movements
		var x = (e.pageX - this.OldX)*MouseSensitivity;
		var y = (this.OldY - e.pageY)*MouseSensitivity;
		
		
		var trigger = e.which || e.keyCode;		// combines mouse and keyboard adjust		
		
			if(this.mouseButton == mouse.brightness){	// contrast and brightness
				
				if(trigger == 1){  // check if mouse triggered the event (1)
					this.currentBrightness = (this.currentBrightness + x);
					this.currentContrast = this.currentContrast + y/4;
				}

				if(this.currentBrightness > 100){
					this.currentBrightness = 100;
				} else if(this.currentBrightness < -100){
					this.currentBrightness = -100;
				}
				
				if(this.currentContrast > 10){
					this.currentContrast = 10;
				} else if(this.currentContrast < 0){
					this.currentContrast = 0;
				}
				
				
				this.updateImage();
			}	
			else if(this.mouseButton == mouse.zoom){	// zoom

				var zoomfactor = 4;	// zoom sensitivity
				
				if(trigger == 1){
					this.currentZoom = this.currentZoom + y/zoomfactor;
					
					// set cursor style
					if(this.OldY - e.pageY > 0){
						document.getElementById("display"+this._imageId).style.cursor = "zoom-in"; 
					}else if(this.OldY - e.pageY < 0){
						document.getElementById("display"+this._imageId).style.cursor = "zoom-out"; 
					}
				}
				
				
				// set margin values
                if (this.currentZoom < .1){
					this.currentZoom = .1;
				}
                else if (this.currentZoom > 15){
					this.currentZoom = 15;
				}else if (this.currentZoom == 0.6){
					this.currentZoom = 0.5;
				}
				
				
                if (this.currentRotation == 90 || this.currentRotation == 270) {
                    this.currentHeight = (this._origWidth * this.currentZoom);
                    this.currentWidth = (this._origHeight * this.currentZoom);
                }
                else {
                    this.currentHeight = (this._origHeight * this.currentZoom);
                    this.currentWidth = (this._origWidth * this.currentZoom);
                }
				
				
							
			
				
				//fudge factor for correcting the image position
				if(this.screenWidth < (this.currentWidth)){
					this.currentMarginLeft = ((this.screenWidth - this.currentWidth)/2) - ((this._origWidth/(2*zoomfactor) - 5));
				}else if(this.screenWidth >= (this.currentWidth)){
					this.currentMarginLeft = 0;
				}
				

		}
		        else if (this.mouseButton == mouse.move) {	// check if key / mouse triggered the event (1)
					
				if(trigger == 1){	
                this.currentMarginLeft += x;
                this.currentMarginTop -= y;
				}

            }
			
				
			// smoothen image if activated
			var smooth = 0;
			
			if(smoothenImage){
				if(this.currentZoom > 3){
						smooth = 1;
					if(this.currentZoom > 5){
						smooth = 2;
						if(this.currentZoom > 7.5){
							smooth = 3;
							if(this.currentZoom > 10){
								smooth = 4;
								if(this.currentZoom > 12.5){
									smooth = 5;
								}
							}
						}
					}
				}

				
			}
			
			    // not all browser can handle document.getElementsByClass.. workaround:
                var allElements = document.getElementsByTagName("*");
                for (var i = 0; i < allElements.length; i++) {
                    if (allElements[i].className == "image") {	
						if(allElements[i].id == "img"+this._imageId){					
							allElements[i].style.width = this.currentWidth + "px";
							allElements[i].style.height = this.currentHeight + "px";
							allElements[i].style.marginTop =   -(this.currentHeight / 2) + this.currentMarginTop + "px";
							allElements[i].style.marginLeft =   -(this.displayLength) + this.currentMarginLeft + "px";
						}
                    }
                }
				
			// alert(document.getElementById("idWrapperContainer").clientWidth+" | "+this.displayLength);
			// console.log("zoom: " + this.currentZoom + " | height: " + this.currentHeight + " | width: " + this.currentWidth + " | margin-left: " + document.getElementById("img" + this._imageId).style.marginLeft + " | margin-top: " + document.getElementById("img" + this._imageId).style.marginTop);
			
			$('#img'+this._imageId).css('filter','blur(' + smooth + 'px) invert(0)');
				this.updateImageInfo();
				
				// save cursor position
				this.OldX = e.pageX;
                this.OldY = e.pageY;
	}	 
}
	
	

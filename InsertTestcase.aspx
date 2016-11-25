<%-- 
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information    
--%>
<%@ Page Title="Testcase" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeBehind="InsertTestcase.aspx.cs" Inherits="LabelingFramework.InsertTestcase" 
	EnableEventValidation="true" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <script type="text/javascript">
	var count = 0;
	var verScale = new Array();
        $(document).ready(function () {
		$('#lblGlobRef').hide();
		CreateReference();
		if($('#MainContent_cbxActiveLearning').is(':checked')){
			$('#trActiveLearning').show();
		}else{
			$('#trActiveLearning').hide();
			}
			
			 
            $('#HiddenFieldIsPatientChosen').attr("value", "false");
            $('#HiddenFieldPathSel').attr("value", "");

            $('#rblTypeScale input').click(function () {


                var valSel = $('#rblTypeScale input:checked').val();

                if (valSel == 0) {
                    $('#ContinuousScale').hide();
					$('#gvContinuous').hide();
                    $('#DiskreteScale').show();
                            }
                else if (valSel == 1) {
                    $('#ContinuousScale').show();
					$('#gvContinuous').show();
                    $('#DiskreteScale').hide();
                }
            });
			
			// $('#cbxActiveLearning input').click(function () {


                // var valSel = $('#rblTypeScale input:checked').val();

                // if (valSel == 0) {
                    // $('#ContinuousScale').hide();
                    // $('#DiskreteScale').show();
                // }
                // else if (valSel == 1) {
                    // $('#ContinuousScale').show();
                    // $('#DiskreteScale').hide();
                // }
            // });
			
			$('#MainContent_cbxActiveLearning').on('click', function() {
				alert("please mind: \nwhen using active learning to only use a single group (if multiple groups are added only the first will be considered)");
				if($(this).is(':checked')){
					$('#trActiveLearning').show();
				}else{
				$('#trActiveLearning').hide();
				}
				
			
			});
			
			// determine which case is selected
			$('input[type=checkbox][id*=cbxSelect]').on('click', function() {
				$('input[type=checkbox][id*=cbxSelect]').not(this).prop('checked', false);
				var id = this.id;
				
				if($(this).is(':checked')){					
					id = id.replace("MainContent_cbxSelect", "");					
				}else{
					id = 0;
				}
				document.getElementById("hddImagesPerPage").value = id;
			});

			
			
			
            //click on add inserts the infos into the hidden panels, c# can take them from here 
            $('#btnAdd').on({
                'click': function () {
							fVersionScale();
                    $("input[type=checkbox][class=cbxsingle]").each(function () {
                        if ($(this).is(':checked')) {
                            var path = $(this).attr("path");
                            var value = $("#HiddenFieldPathSel").val();
                            $("#HiddenFieldPathSel").attr("value", path + "|" + value); // - Martin
                            //$(this).attr("checked",false)
							
                        }
                    });

					var counter = 0;
					var counter2 = 0;
					var first = true;
					var patChos = false;
                    $("input[type=checkbox][class=cbxall]").each(function () {
						
                        if ($(this).is(':checked')) {
							counter2++;
							patChos = true;
                            $("#HiddenFieldIsPatientChosen").attr("value", "true");
                            //$(this).attr("checked", false)
							
							if(first){
							$("input[type=checkbox][class=cbxsingle]").each(function () {
							if($(this).is(':checked')){	
								counter++;
							}else{
							}
								
							});
								
								first = false;
							}
                        }
                    });
					
					
					if(patChos){
						
						count = Math.floor(counter/counter2);
						
					}

                    var reference = $("#selReference option:selected").text();
					
					
                    $("#HiddenFieldReferenceName").attr("value", reference);
                    $("#hiddenselect").removeAttr("value");
					$("#hddNumberImagePatient").attr("value", count); 
					
					
					// var test = document.getElementById("hddImagesPerPage").value;
					// var test2 = document.getElementById("txtmImagesPerPage").value;
					
		// alert("Seitenstruktur:\n\nimages selected per patient: " + count + "\ncase: " + test + "\nimages per page: " + test2);

					
                }
            });
			
        });


        $(document).ready(function () {
            // Send an AJAX request
            // $.ajax({
                // url: 'api/directory',
                // type: 'GET',
                // dataType: 'json',
                // success: function (data) {
                    // BuildTable(data);

                // },
                // error: function () {
                    // alert('Something went wrong with ready');
                // }
            // });
            //$.getJSON("api/Directory",
            //  function (data) {
            // On success, 'data' contains a list of products.
            //      BuildTable(data);
            //  });

			
			if(!$('#MainContent_cbxSelect1').is(':checked')){
				$('#MainContent_cbxSelect0').attr("checked", true);
			}
			
			
        });

        function fUpdateTable() {
            $.ajax({
                url: 'api/directory',
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    BuildTable(data);
					fVersionScale();
                },
                error: function () {
                    alert('Something went wrong with ready');
                }
            });
        }
		
		
			
			
		   function fVersionScale() {
			$('[id*=MainContent_gvContinuous_checkBox_]').each(function() {	

			var id = this.id;
			var that = this;	
			
			id = id.replace("MainContent_gvContinuous_checkBox_", "");
			var e = document.getElementById('MainContent_gvContinuous_ddlContinuous_' + id);	
			if($(this).is(':checked')){	
				verScale.push(e.options[e.selectedIndex].value);
			}else{
				verScale.push("-1");
			}
				});	
				$("#HiddenFieldScaleVersions").attr("value", JSON.stringify(verScale));
		   }	
			
           // searches sequences folders like TSE, FLASH,
        function find() {
            var id = $('#searchText').val() +"!";
            $.ajax({
                url: 'api/directory/' + id,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    BuildTable(data);
                },
                error: function () {
                    alert('Something went wrong with find');
                }
            });
        }


        function findbyId(id) {
				
            $.ajax({
                url: 'api/directory/' + id,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
				if(data == ""){
					alert("received empty package");
				}
				
		console.log("package: " + data);
                    BuildTable(data);
                },
                error: function () {
                    alert('Something went wrong with findbyID');
                }
            });
        }

        function BuildTable(msg) {
            ClearAll();
            var table = '<table><thead><tr><th>Name</th></thead><tbody>';
            var arr = $("#hiddenselect").val().split('!');
           // var arr = "s";

            for (var post in msg) {
                var row = '<tr>';

                row += '<td> <img alt = "" style="cursor: pointer" src="managementImg/imgHelp/plus.png" /></img></td>';
                row += '<td>' + msg[post].NameDirectory + '</td>';
                row += '<td> <input type="checkbox" id="cbxall_' + msg[post].IdDirectory + '" class="cbxall" onchange="javascript:OnChangeAll(this);" value="' + +msg[post].IdDirectory + '"></input> </td>';
                row += '</tr>';

                for (var sub in msg[post].SubDirectory) {
                    var isChecked = 0;
                    row += '<tr>';
                    row += '<td></td>';
                    row += '<td></td>';
                    row += '<td>' + msg[post].SubDirectory[sub].NameDirectory + '</td>';

                    for (var checkvalue in arr) {
                        if (arr[checkvalue].toLowerCase() == msg[post].SubDirectory[sub].NameDirectory.toLowerCase()) {
                            isChecked = 1;
                            break;
                        }
                        else {
                            isChecked = 0;
                        }
                    }


                    if (isChecked == 1) {
                        row += '<td> <input type="checkbox" id="cbxsel_' + msg[post].SubDirectory[sub].NameDirectory + 	'" value="' + +msg[post].SubDirectory[sub].IdDirectory + '" name="' + msg[post].SubDirectory[sub].NameDirectory + '" onchange="javascript:OnChangeSingle(this);" checked class="cbxsingle" path="' + msg[post].NameDirectory + '/' + msg[post].SubDirectory[sub].NameDirectory + '"></input> </td>';
                    }
                    else {
                        row += '<td> <input type="checkbox" id="cbxsel_' + msg[post].SubDirectory[sub].NameDirectory + '" value="' + +msg[post].SubDirectory[sub].IdDirectory + '" name="' + msg[post].SubDirectory[sub].NameDirectory + '" onchange="javascript:OnChangeSingle(this);" class="cbxsingle" path="' + msg[post].NameDirectory + '/' + msg[post].SubDirectory[sub].NameDirectory + '"></input > </td>';
                    }

                    row += '</tr>';
                }
                table += row;
            }
            table += '</tbody></table>';
            $('#Container').html(table);
        }

        // checked patient name
        function OnChangeAll(obj) {
            var number = $("#HiddenNumberPatient").val();

            if ($(obj).is(':checked')) {
                var valuesel = $(obj).val();
                number++;

                $("input[type=checkbox][value=" + valuesel + "]").prop('checked', true);
            }
            else {
                number--;

                var valuesel = $(obj).val();
                
                $("#HiddenAllinComune").attr("value", "");
                $("input[type=checkbox][value=" + valuesel + "]").prop('checked', false);
            }

            $("#HiddenNumberPatient").attr("value", number);
            CreateReferenceAll();
        }

        // checked single folder not patient name
        function OnChangeSingle(obj) 
        {
            if ($(obj).is(':checked')) {

                var namesel = $(obj).attr("name");
                $("#hiddenselect").attr("value", $("#hiddenselect").val() + namesel + "!");
				count++;
             
            }
            else {
                var namesel = $(obj).attr("name");
				count--;
                var str = $("#hiddenselect").val()
                var res = str.replace(namesel + "!", "");

                $("#hiddenselect").attr("value", res);
                $("input[type=checkbox][name=" + namesel + "]").prop('checked', false);
            }
            findbyId($("#hiddenselect").val());
			
                  CreateReference();
        }


        function CreateReference() {

        
            var arr = $("#hiddenselect").val().split('!');

            if (arr.length - 1 > 0) 
              {
            var html = '<select id="selReference">';
            if (arr.length - 1 == 1) {
                html += "<option  value='" + 0 + "'>Choose a Reference</option>";
                html += "<option  value='" + 1 + "'>Global</option>";

				html += "<option  value='" + 2 + "'>" + arr[0] + "</option>";
                html += "</select>";
                $('#reference').html(html);
            }

            if (arr.length - 1 > 1) {

                html += "<option  value='" + 0 + "'>Choose a Reference</option>";
                html += "<option  value='" + 1 + "'>Global</option>";

                var i = 3;
                for (var checkvalue in arr) {
                    i++;
					if(arr[checkvalue].toUpperCase() != ""){
					html += "<option  value='" + i + "'>" + arr[checkvalue].toUpperCase() + "</option>";
					}
                    
                }

                html += "</select>";
		
                $('#reference').html(html);
            }


        }
        else
            $('#reference').empty();


			$('#selReference').on('change', function () {
	        var reference = $("#selReference option:selected").text();
		if(reference == "Global"){
			$('#MainContent_cbxDiscreteScale').css("display", "inline");
			$('#lblGlobRef').show();
			
		}else{
			$('#MainContent_cbxDiscreteScale').css("display", "none");	
			$('#lblGlobRef').hide();
		}
	
	
		return false;
	});

        }

        function CreateReferenceAll() {
            var html = '<select id="selReference">';

            var number = $("#HiddenNumberPatient").val();
            var numberchecked = 0;
            $("#HiddenAllinComune").attr("value", "");
            
            if (number > 1) {
                $("input[type=checkbox][class=cbxsingle]").each(function () {

                    if ($(this).is(':checked')) {
                        var found = 0;
                        numberchecked++;
                        var str = $(this).attr("name");

                        $("input[type=checkbox][class=cbxsingle]").each(function () {



                            if ($(this).is(':checked')) {
                                var strapp = $(this).attr("name");

                                if (str == strapp) {
                                    found++;
                                }
                            }

                        });

                        if (found == number) 
                        {

                            var value = $("#HiddenAllinComune").val();

                            if (value.toLowerCase().indexOf(str.toLowerCase() + "!") < 0) {
                                $("#HiddenAllinComune").attr("value",$("#HiddenAllinComune").val() + str + "!");

                           }
                        }
                       
                    }
                });



                //if (numberchecked>1)

                var arr = $("#HiddenAllinComune").val().split('!');
                html += "<option  value='" + 0 + "'>Choose a Reference</option>";
                html += "<option  value='" + 1 + "'>Global</option>";
                var i = 1;
                for (var checkvalue in arr) {
                    i++;
                    html += "<option  value='" + i + "'>" + arr[checkvalue].toUpperCase() + "</option>";

                }



                html += "</select>";
                $('#reference').html(html);
            }
            else if (number == 1) {

                html += "<option  value='" + 0 + "'>Choose a Reference</option>";
                html += "<option  value='" + 1 + "'>Global</option>";
                html += "</select>";
                $('#reference').html(html);
            }
            else if (number == 0) {

               
                $('#reference').empty();
            }
        }


        function ClearAll() {


            $("input[type=checkbox][class=cbxsingle]").each(function () {

                $(this).removeAttr("checked", false)
                
            });

            $("input[type=checkbox][class=cbxall]").each(function () {

 
                    $(this).removeAttr("checked")
                
            });
        
        }   
        



  </script>




    <%--all the errors of the page--%>
    
<asp:ValidationSummary ID="TestCaseValidationSummary" runat="server" CssClass="validation" 
                 ValidationGroup="TestCaseValidationGroup"/>

                  <div class="success"  runat="server" id="DivSuccess" visible="false">
                            <asp:Label id="lblSuccess" runat="server" Text="Insert successfully completed!"></asp:Label>
                    </div>

                    <div class="error"  runat="server" id="DivError" visible="false">
                            <asp:Label id="lblError" runat="server" Text=""></asp:Label>
                    </div>
                 
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    
                    <div class="accountInfo">
                        <fieldset class="register" style="width: 97%;">
                            <legend>Insert Testcase</legend>
                            <table>                             
                            <tr>
                            <td><asp:Label ID="lblNameTestCase" runat="server" Text="Test Case Name:"></asp:Label></td>
                            <td><asp:TextBox ID="txtNameTestCase" runat="server" TextMode="SingleLine" Width="300px" maxlength="45"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNameTestCase" runat="server" ControlToValidate="txtNameTestCase" ForeColor="Red" ErrorMessage="Test Case Name is Required!." ValidationGroup="TestCaseValidationGroup">*</asp:RequiredFieldValidator>
                                   <asp:CustomValidator ID="CVTestCaseName" runat="server" 
                                    onservervalidate="CVTestCaseName_ServerValidate" ValidationGroup="TestCaseValidationGroup"  ErrorMessage="This Test Case Name is already in use, please choose a different one" ForeColor="Red" Display="Dynamic">*</asp:CustomValidator>
                            </td>
                            </tr>
                            <tr>                             
                            <td><asp:Label ID="lblTestQuestion" runat="server" Text="Test Question:"></asp:Label></td>
                            <td><asp:TextBox ID="txtTestQuestion" runat="server" TextMode="SingleLine" Width="300px" maxlength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTestQuestion" runat="server" ControlToValidate="txtTestQuestion" ForeColor="Red" ErrorMessage="Test Question is Required!." ValidationGroup="TestCaseValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                            </tr>

                            <tr>
                               
                             <td><asp:Label ID="lDatabase" runat="server" Text="Database:"></asp:Label></td>
                             <td><asp:DropDownList Width="300px" ID="ddlDatabase" runat="server" OnSelectedIndexChanged="ddlDatabase_selectedIndexChanging" AutoPostBack="true"></asp:DropDownList></td>
                                                              
                            </tr>
							<tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                            <tr>
                            <td><asp:Label ID="lblDiskreteScale" runat="server" Text="Type Scale:"></asp:Label></td>
                            <td><asp:RadioButtonList ID="rblTypeScale" runat="server" RepeatDirection="Vertical" Width="300px" ClientIDMode="Static" OnSelectedIndexChanged="rblTypeScale_selectedIndexChanging" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="Discrete Scale"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Continuous Scale"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvTypeScale" runat="server"  ValidationGroup="TestCaseValidationGroup" ControlToValidate="rblTypeScale" ForeColor="Red" ErrorMessage="Please choose a scale.">*</asp:RequiredFieldValidator>
                            </td>
                            </tr>
                            <tr id="trDiscrete" runat="server" visible="false">                        
                                <td><asp:Label ID="lblMinMaxDiskreteScale" runat="server" Text="Discrete Scale:" Visible="true"></asp:Label></td>
                                <td>Min:&nbsp; <asp:TextBox ID="txtminvalue" runat="server" ClientIDMode="Static" Width="50px" Text="1" ></asp:TextBox>
                                     <asp:CompareValidator runat="server" ID="cvMinVal1" ControlToValidate="txtminvalue" ControlToCompare="txtmaxvalue" operator="LessThan" Type="Integer" ErrorMessage="Min must be smaller than Max!" />
                                     
                                Max:&nbsp; <asp:TextBox ID="txtmaxvalue" runat="server" ClientIDMode="Static"  Width="50px" ></asp:TextBox>
								<asp:CompareValidator runat="server" ID="cvMinVal2" ControlToValidate="txtminvalue" ValueToCompare="0" operator="GreaterThan" Type="Integer" ErrorMessage="Min must be greater than zero!" />
								&nbsp;&nbsp;
                                <label id = "lblGlobRef">Global reference:</label>&nbsp;<asp:DropDownList ID="cbxDiscreteScale" runat="server"></asp:DropDownList></td>
								<td>&nbsp;</td>
                            </tr>
                             <tr id="trContinuous" runat="server" visible="false"> 
                                <td><asp:Label ID="lblContinuousScale" runat="server" Text="Continuous Scale:" Visible="true"></asp:Label></td>
                                <td>
								<div align="left">
								<asp:GridView ID="gvContinuous" runat="server" DataKeyNames="ID_TypScaleContinuous" OnDataPropertyChanged="gvContinuous_OnDataPropertyChanged" OnRowDataBound="OnRowDataBound" AutoGenerateColumns="false" width="35%" HorizontalAlign="left">
    <Columns >
	
        <asp:BoundField DataField="DescriptionScaleContinuous"  HeaderText="description"/>

		<asp:TemplateField  HeaderText="use">
             <ItemTemplate>	
             <asp:Checkbox ID="checkBox"  AutoPostBack="true" OnCheckedChanged="Check_Clicked" runat="server"/>
			</ItemTemplate>
         <ItemStyle CssClass="gvItemCenter"/>
         <HeaderStyle CssClass="gvItemCenter"/>
         </asp:TemplateField>
		 
		 <asp:TemplateField  HeaderText="select scale version">
             <ItemTemplate>	
             <asp:DropDownList  Width="50px" ID="ddlContinuous" OnSelectedIndexChanged="ddlContinuous_selectedIndexChanging" AutoPostBack="true" runat="server"/>
			 </asp:DropDownList>
			</ItemTemplate>
         <ItemStyle CssClass="gvItemCenter"/>
         <HeaderStyle CssClass="gvItemCenter"/>
         </asp:TemplateField>

		 

		
    </Columns>
</asp:GridView>
</div>
								</td>
                            </tr>
							<tr><td>&nbsp;</td><td>&nbsp;</td></tr>
							<tr>
							<td><label>Reference:</label></td>
							<td><div id="reference"></div></td>
							</tr>
							<tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                            <tr>

                               <td><asp:Label ID="lblTypeUserAssigned" runat="server" Text="Assign to user type:"></asp:Label></td>
                               <td><asp:CheckBoxList ID="rblTypeUserAssigned" runat="server" ></asp:CheckBoxList>
                                <asp:CustomValidator ID="CVTypeUserAssigned" runat="server"  
                                    ValidationGroup="TestCaseValidationGroup" 
                                    ForeColor="Red" 
                                    ErrorMessage="Please choose a Type User Assigned." 
                                    onservervalidate="CVTypeUserAssigned_ServerValidate">*</asp:CustomValidator>
                               
                            </td>
                            </tr>
                             <tr>
                               
                                <td><asp:Label ID="lblActiveLearning" runat="server" Text="Active Learning:"></asp:Label></td>
                                <td><asp:CheckBox ID="cbxActiveLearning" runat="server" /></td>
                               
                            </tr>
							  <tr id="trActiveLearning" visible="false"> 

								<td></td><td>
								<asp:Label ID="lblActiveInitialThreshold" runat="server" Text="initial threshold: "></asp:Label>
								<asp:TextBox ID="txtActiveInitialThreshold" runat="server" ClientIDMode="Static" Width="50px" Text="0" ></asp:TextBox>
								<asp:Label ID="lblActiveUserThreshold" runat="server" Text="user threshold: "></asp:Label>
								<asp:TextBox ID="txtActiveUserThreshold" runat="server" ClientIDMode="Static" Width="50px" Text="0" ></asp:TextBox>
								</td>
							</tr>
                             <tr>
                               
                                <td><asp:Label ID="lblGeneralInfo" runat="server" Text="General Information:"></asp:Label></td>
                                <td><asp:TextBox ID="txtGeneralInfo" runat="server" TextMode="MultiLine" Width="500px" maxlength="200"></asp:TextBox></td>
                               
                            </tr>
                          </table>
                           
                            <asp:HiddenField ID="hddTCSeed" runat="server" Value="" ClientIDMode="Static" />
                        </fieldset>
                      
                    </div>
					<div>			
<br>		

  <p>
                               <asp:Button ID="btnInsertTestCase" runat="server" Text="Insert" ValidationGroup="TestCaseValidationGroup"
                                    onclick="btnInsertTestCase_Click" ToolTip="Insert Testcase into Database"/>
                                    <asp:Button ID="btnUpdateTestCase" runat="server" Text="Update"  Visible="false" onclick="btnUpdateTestCase_Click" ToolTip="Save Changes to Database" />
                            </p>
</div>
 <fieldset style="width:40%;float:left;" >
 <legend>Management Loop</legend>
 
 
    <%--<div>
    <asp:Label ID="lblSearchSequence" runat="server" Text="Search Sequence"></asp:Label>
    <asp:TextBox ID="txtSearchSequence" runat="server" Text="" maxlength="200"></asp:TextBox>

    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
    </div>--%>

    <div style="width:100%;vertical-align:top;" >
    <div style="width:50%;float:left;">  

 

        <input type="text" id="searchText"  />
        <input type="button" id="Search"  onclick="find();" value="Search"/>

         <asp:HiddenField ID="hiddenselect" runat="server" Value="" ClientIDMode="Static" />
         <asp:HiddenField ID="HiddenNumberPatient" runat="server" Value="0" ClientIDMode="Static" />
         <asp:HiddenField ID="HiddenAllinComune" runat="server" Value="" ClientIDMode="Static" />
         <asp:HiddenField ID="HiddenFieldPathSel" runat="server" Value="" ClientIDMode="Static" />
         <asp:HiddenField ID="HiddenFieldReferenceName" runat="server" Value="" ClientIDMode="Static" />
         <asp:HiddenField ID="HiddenFieldIsPatientChosen" runat="server" Value="False" ClientIDMode="Static" />
		 <asp:HiddenField ID="hddNumberImagePatient" runat="server" Value="0" ClientIDMode="Static" />
		 <asp:HiddenField ID="HiddenFieldScaleVersions" runat="server" Value="" ClientIDMode="Static" />
		 
         <div id="Container">

    </div>

  </div>
     <div style="width:50%;float:left;">
          <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ClientIDMode="Static" />
		  <fieldset>
		  <table>
		<tr>
		 <td><asp:CheckBox ID="cbxSelect0" runat="server"/></td> 	
         <td><asp:Label ID="lblPatientPerPage" runat="server" Text="patient per page "></asp:Label></td>	 
        </tr>
		
		<tr>
         <td><asp:CheckBox ID="cbxSelect1" runat="server" /></td>   
		 <td><asp:TextBox ID="txtmImagesPerPage" runat="server" ClientIDMode="Static" Width="50px" Text="1" ></asp:TextBox>
		 <asp:Label ID="lblImagesPerPage" runat="server" Text="images per page"></asp:Label></td>
		</tr>
			
		<asp:HiddenField ID="hddImagesPerPage" runat="server" Value="0" ClientIDMode="Static" />
		  </fieldset>
		  </table>
 </div>
    </div>
  </fieldset>
  

  <fieldset style="Width:58%;float:right;">
  <legend>Groups</legend>
 
            
     <asp:GridView ID="gvGroupName" runat="server" DataKeyNames="IdGroup" AutoGenerateColumns="false" Width="100%">
     <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="ReferenceName" HeaderText="ReferenceName" />
		<asp:BoundField DataField="PageStyleDescription" HeaderText="(images to label) / page" />
        <asp:BoundField DataField="LoopOver" HeaderText="LoopOver" />
		<asp:BoundField DataField="OverallNumber" HeaderText="overall number of images" />
		<asp:BoundField DataField="NumberOfPages" HeaderText="number of pages" />
		
		<asp:TemplateField HeaderText="remove group" >          
          <ItemTemplate>
             <asp:Button ID="btnRemoveGroup" text="remove" runat="server" CommandArgument='<%# Bind("IdGroup") %>'  CommandName ="Remove" OnClick="RemoveGroup_Click" />
          </ItemTemplate>  
        </asp:TemplateField>
		
     </Columns>
     
     
     </asp:GridView>
      </fieldset>

       

</asp:Content>

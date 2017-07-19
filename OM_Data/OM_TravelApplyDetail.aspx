<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_TravelApplyDetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_TravelApplyDetail"
    Title="差旅信息" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    差旅信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/Setting.css" />
    <link href="StyleFile/Style.css" rel="stylesheet" type="text/css" />

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
    
       function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
        //**********弹出人员子窗口***********************
        var i;
        array = new Array();
        function SelectPerson1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        function SelectPerson2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
        function SelectPerson3() {
            $("#hidPerson").val("third");
            SelPersons();
        }

        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
        
        function TestSave(){
           if($("#<%=firstid.ClientID %>").val()==""||$("#<%=secondid.ClientID %>").val()==""||$("#<%=thirdid.ClientID %>").val()==""){
             alert('请选择审批人后再提交审批！');
             return false;
          }
            return true;
        }
        function TestSubmit(){
         if($("#<%=rblSPJB.ClientID %>").attr("disabled")=="disabled"){
            if($("#<%=rblResult1.ClientID %>").find("[checked]").val()!="0"&&$("#<%=rblResult1.ClientID %>").find("[checked]").val()!="1"){
               alert('请选择同意或不同意！');
               return false;
               }
            if($("#<%=rblSPJB.ClientID %>").find("[checked]").val()=="2"){
             if(($("#<%=rblResult2.ClientID %>").find("[checked]").val()!="0"&&$("#<%=rblResult2.ClientID %>").find("[checked]").val()!="1")&&$("#<%=rblResult1.ClientID %>").attr("disabled")=="disabled"){
               alert('请选择同意或不同意！');
               return false;
                }
               }
            if($("#<%=rblSPJB.ClientID %>").find("[checked]").val()=="3"&&$("#<%=rblResult3.ClientID %>").find("[checked]").val()!="0"&&$("#<%=rblResult3.ClientID %>").find("[checked]").val()!="1"){
              if($("#<%=rblResult2.ClientID %>").attr("disabled")=="disabled"){
               if($("#<%=rblResult1.ClientID %>").find("[checked]").val()!="0"&&$("#<%=rblResult1.ClientID %>").find("[checked]").val()!="1"){
                  alert('请选择同意或不同意！');
                  return false;
                    }
               if($("#<%=rblResult3.ClientID %>").find("[checked]").val()!="0"&&$("#<%=rblResult3.ClientID %>").find("[checked]").val()!="1"&&$("#<%=rblResult1.ClientID %>").attr("disabled")=="disabled"){
                  alert('请选择同意或不同意！');
                  return false;
                    }
                  }
              if($("#<%=rblResult2.ClientID %>").attr("disabled")!="disabled"){                
               if($("#<%=rblResult2.ClientID %>").find("[checked]").val()!="0"&&$("#<%=rblResult2.ClientID %>").find("[checked]").val()!="1"){
                  alert('请选择同意或不同意！');
                  return false;
                }
               }            
             }  
          }
         return true;
      }
      function getendtimep(obj){
        var days= obj.value;
        var pattem = /^[1-9][0-9]*$/; //数量验证
            var days = obj.value;
            if (!pattem.test(days)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
                days="1";
            }
        var exedate='2015-11-20';
        var result = exedate.match(/((^((1[8-9]\d{2})|([2-9]\d{3}))(-)(10|12|0?[13578])(-)(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(-)(11|0?[469])(-)(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(-)(0?2)(-)(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)(-)(0?2)(-)(29)$)|(^([3579][26]00)(-)(0?2)(-)(29)$)|(^([1][89][0][48])(-)(0?2)(-)(29)$)|(^([2-9][0-9][0][48])(-)(0?2)(-)(29)$)|(^([1][89][2468][048])(-)(0?2)(-)(29)$)|(^([2-9][0-9][2468][048])(-)(0?2)(-)(29)$)|(^([1][89][13579][26])(-)(0?2)(-)(29)$)|(^([2-9][0-9][13579][26])(-)(0?2)(-)(29)$))/);//验证日期格式2015-11-20
        if(result==null)
        {
            alert("请输入正确的日期格式");
            return false;
        }
        var tr=obj.parentNode.parentNode;
        var startdate = tr.getElementsByTagName("td")[4].getElementsByTagName("input")[1]; 
        if(startdate.value==""){
            alert("请输入预计出差起始时间!");
            return false;
              }
        else{
         var enddatereal = tr.getElementsByTagName("td")[5].getElementsByTagName("input")[2];
         var enddate = tr.getElementsByTagName("td")[5].getElementsByTagName("input")[1];
         var startdate = startdate.value.split("-");          
         var nDate = new Date(startdate[1] + '-' + startdate[2] + '-' + startdate[0]); //转换为MM-DD-YYYY格式    
         var millSeconds = Math.abs(nDate) + ((days-1) * 24 * 60 * 60 * 1000); 
         var rDate = new Date(millSeconds);  
         var year = rDate.getFullYear();  
         var month = rDate.getMonth() + 1;  
         if (month < 10) month = "0" + month;  
         var date = rDate.getDate();  
         if (date < 10) date = "0" + date;  
         enddatereal.value=year + "-" + month + "-" + date;
         enddate.value=year + "-" + month + "-" + date;
        }
      }
       
      function getendtimer(obj){
        var days= obj.value;
        var pattem = /^[1-9][0-9]*$/; //数量验证
            var days = obj.value;
            if (!pattem.test(days)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
                days="1";
            }
        var tr=obj.parentNode.parentNode;
        var startdate = tr.getElementsByTagName("td")[9].getElementsByTagName("input")[1]; 
        if(startdate.value==""){
            alert("请输入实际出差起始时间!");
            return false;
              }
        else{
         var enddatereal = tr.getElementsByTagName("td")[10].getElementsByTagName("input")[2];
         var enddate = tr.getElementsByTagName("td")[10].getElementsByTagName("input")[1];
         var startdate = startdate.value.split("-");          
         var nDate = new Date(startdate[1] + '-' + startdate[2] + '-' + startdate[0]); //转换为MM-DD-YYYY格式    
         var millSeconds = Math.abs(nDate) + ((days-1) * 24 * 60 * 60 * 1000); 
         var rDate = new Date(millSeconds);  
         var year = rDate.getFullYear();  
         var month = rDate.getMonth() + 1;  
         if (month < 10) month = "0" + month;  
         var date = rDate.getDate();  
         if (date < 10) date = "0" + date;  
         enddatereal.value=year + "-" + month + "-" + date;
         enddate.value=year + "-" + month + "-" + date;
        }
      }
      
      $(function(){
       var rows=document.getElementById("gr").rows;  
        if(rows.length>1){    
            for(var i=1;i<rows.length;i++){  
              (function(i){  
                var obj=rows[i].cells[6].childNodes[1]; 
                if(obj.value!="")
                   getendtimep(obj);
                var obj=rows[i].cells[11].childNodes[1]; 
                if(obj.value!="")
                   getendtimer(obj);
                })(i)  
            }  
        }  
      })
        
    </script>

    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 250px;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
            width: 250px;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
            width: 250px;
        }
        .center
        {
            text-align: center;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="float: right">
        <asp:Button ID="btnSubmit" runat="server" Text="提交审批" OnClientClick="return TestSubmit();"
            OnClick="btnSubmit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSave" runat="server" Text="保 存" OnClientClick="return TestSave();"
            OnClick="btnSave_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnBack" onclick="window.location.href='OM_TravelApply.aspx';"
            value="返 回" />
        &nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="基本信息" TabIndex="0">
            <HeaderTemplate>
                基本信息
            </HeaderTemplate>
            <ContentTemplate>
                <asp:Panel ID="PanelDe" runat="server">
                    <div class="box-wrapper1">
                        <div class="box-outer" style="text-align: center; overflow-x: hidden">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 90%;" align="center">
                                        <h2>
                                            <asp:Label runat="server" ID="lblType" Text="差旅申请单"></asp:Label>
                                        </h2>
                                    </td>
                                    <td style="width: 10%;" align="center">
                                        <asp:CheckBox ID="chkSure" runat="server" Checked="true" Font-Size="10px" ForeColor="Red"
                                            Text="不需确认" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Panel ID="Panel0" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 10%" align="center">
                                            &nbsp; &nbsp; &nbsp; 申请单号：
                                            <asp:Label ID="lbTA_Code" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 15%" align="center">
                                            &nbsp; &nbsp; &nbsp; &nbsp; 制单人姓名：
                                            <asp:Label ID="lbTA_ZDR" runat="server"></asp:Label>
                                            <input type="hidden" id="HidTA_ZDRID" runat="server" />
                                            &nbsp; &nbsp; &nbsp; &nbsp; 制单人部门：
                                            <asp:Label ID="lbTA_ZDRDep" runat="server"></asp:Label>
                                            <input type="hidden" id="HidTA_ZDRDepID" runat="server" />
                                        </td>
                                        <td style="width: 15%" align="center">
                                            制单时间：<asp:Label ID="lbTA_ZDTime" runat="server" Text=""></asp:Label>
                                            &nbsp; &nbsp;&nbsp;确认时间：<asp:Label ID="lbTA_SureTime" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="Panel1" runat="server">
                                <div style="text-align: center; overflow: auto; height: 355px">
                                    <table id="gr" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                        <asp:Repeater ID="rptTravelDetail" runat="server" OnItemDataBound="rptTravelDetail_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr class="tableTitle headcolor" height="30px">
                                                    <td align="center">
                                                        <asp:Label ID="Label" Text="序 号" runat="server" Font-Bold="true" Width="50px"></asp:Label>
                                                    </td>
                                                    <td align="center" id="th1" runat="server">
                                                        <strong>出差人员</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>所属部门</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>部门职位</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>开始时间(预)</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>结束时间(预)</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>出差天数(预)</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>出差事项</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>出差地点</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>开始时间(实)</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>结束时间(实)</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>出差天数(实)</strong>
                                                    </td>
                                                    <td align="center">
                                                        <strong>备注</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                    <td>
                                                        <div>
                                                            <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                            <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                            </asp:CheckBox></div>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:TextBox ID="txtTA_SQR" runat="server" CssClass="center" Text='<%#Eval("TA_SQR") %>'
                                                            OnTextChanged="SQRName_TextChanged" AutoPostBack="true" Width="80px"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id_new"
                                                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtTA_SQR"
                                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                        </asp:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtTA_SQRID" runat="server" CssClass="center" Text='<%#Eval("TA_SQRID")%>'
                                                            Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtTA_SQRDep" runat="server" CssClass="center" Text='<%#Eval("TA_SQRDep") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtTA_SQRDepPos" runat="server" CssClass="center" Text='<%#Eval("TA_SQRDepPos") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTA_StartTimePlan" Text='<%#Eval("TA_StartTimePlan")%>' runat="server"
                                                            class="easyui-datebox" editable="false" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTA_EndTimePlan" Text='<%#Eval("TA_EndTimePlan")%>' runat="server"
                                                            class="easyui-datebox" editable="false" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTA_DaysPlan" runat="server" Text='<%#Eval("TA_DaysPlan")%>' onblur="getendtimep(this)"
                                                            Width="60px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTA_Event" TextMode="MultiLine" Text='<%#Eval("TA_Event")%>' runat="server"
                                                            Width="240px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTA_Place" TextMode="MultiLine" Text='<%#Eval("TA_Place")%>' runat="server"
                                                            Width="240px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTA_StartTimeReal" Text='<%#Eval("TA_StartTimeReal")%>' runat="server"
                                                            class="easyui-datebox" editable="false" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTA_EndTimeReal" Text='<%#Eval("TA_EndTimeReal")%>' runat="server"
                                                            class="easyui-datebox" editable="false" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTA_DaysReal" runat="server" Text='<%#Eval("TA_DaysReal")%>' onblur="getendtimer(this)"
                                                            Width="60px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTA_Note" TextMode="MultiLine" Text='<%#Eval("TA_Note")%>' runat="server"
                                                            Width="240px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                        没有记录!</asp:Panel>
                                    <div style="float: left; padding-top: 10px">
                                        &nbsp; &nbsp; 增加行:<asp:TextBox ID="txtNum" runat="server" Width="30px" onblur="CheckNum(this);"
                                            CssClass="center"></asp:TextBox>&nbsp;&nbsp;
                                        <asp:Button ID="btnAdd" runat="server" Text="确 定" OnClick="btnAdd_Click" />
                                        &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="审核信息" TabIndex="2">
            <ContentTemplate>
                <asp:Panel ID="PanelSH" runat="server">
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:RadioButtonList runat="server" ID="rblSPJB" RepeatDirection="Horizontal" RepeatColumns="3"
                                        AutoPostBack="true" OnTextChanged="rblSPJB_onchange">
                                        <asp:ListItem Text="一级审批" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="二级审批" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="三级审批" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    差旅申请审批
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <asp:Panel runat="server" ID="Panel2">
                                <tr>
                                    <td align="center">
                                        一级审批
                                    </td>
                                    <td colspan="3">
                                        <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                            <tr style="height: 25px">
                                                <td align="center" style="width: 10%">
                                                    审核人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txt_first" runat="server" Width="80px" onfocus="this.blur()"></asp:TextBox>
                                                    <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelectPerson1()">
                                                        <asp:Image ID="AddImage12" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                            align="absmiddle" runat="server" />
                                                        选择
                                                    </asp:HyperLink>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结果
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList runat="server" ID="rblResult1" RepeatColumns="2">
                                                        <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核时间
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                        Height="42px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="Panel3">
                                <tr>
                                    <td align="center">
                                        二级审批
                                    </td>
                                    <td colspan="3">
                                        <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                            <tr style="height: 25px">
                                                <td align="center" style="width: 10%">
                                                    审核人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txt_second" runat="server" Width="80px" onfocus="this.blur()"></asp:TextBox>
                                                    <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelectPerson2()">
                                                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                            runat="server" />
                                                        选择
                                                    </asp:HyperLink>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结果
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList runat="server" ID="rblResult2" RepeatColumns="2">
                                                        <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核时间
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="second_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                        Height="42px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="Panel4">
                                <tr>
                                    <td align="center">
                                        三级审批
                                    </td>
                                    <td colspan="3">
                                        <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                            <tr style="height: 25px">
                                                <td align="center" style="width: 10%">
                                                    审核人
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txt_third" runat="server" Width="80px" onfocus="this.blur()"></asp:TextBox>
                                                    <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelectPerson3()">
                                                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                            runat="server" />
                                                        选择
                                                    </asp:HyperLink>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核结果
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:RadioButtonList runat="server" ID="rblResult3" RepeatColumns="2">
                                                        <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="width: 10%">
                                                    审核时间
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                        Height="42px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>
                    </div>
                    <div style="color: Red; text-align: center">
                        <strong style="vertical-align: middle">*</strong> &nbsp;&nbsp; 二级审批：部门主管--公司领导&nbsp;&nbsp;&nbsp;&nbsp;
                        三级审批：xxx--部门主管--公司领导
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <div>
        <div>
            <div id="win" visible="false">
                <div>
                    <table>
                        <tr>
                            <td>
                                <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                按部门查询：
                            </td>
                            <td>
                                <input id="dep" name="dept" value="03">
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 430px; height: 230px">
                    <table id="dg">
                    </table>
                </div>
            </div>
            <div id="buttons" style="text-align: right" visible="false">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
</asp:Content>

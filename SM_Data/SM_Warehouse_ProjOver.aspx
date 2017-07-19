<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Warehouse_ProjOver.aspx.cs" MasterPageFile="~/Masters/SMBaseMaster.Master" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_ProjOver" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>
    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/superTables.js" type="text/javascript"></script>

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>
    
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
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>

    <script type="text/javascript" language="javascript">
 var postBack=true;
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args){
 if (args.get_postBackElement().id == "<%= btnClose.ClientID %>"||args.get_postBackElement().id == "<%= btnReset.ClientID %>" ||args.get_postBackElement().id == "<%= DropDownListwarehouse.ClientID %>"||args.get_postBackElement().id == "<%= TextBoxtaskid.ClientID %>")
   {
     postBack=false;
   }
   else
   {
    ActivateAlertDiv('visible', 'AlertDiv', '');
    }
    
}
function EndRequestHandler(sender, args){
if(postBack){
    document.getElementById("GridView1").parentNode.className = "fakeContainer";
document.getElementById("GridView1").parentNode.style.width=window.screen.availWidth-182;
document.getElementById("GridView1").parentNode.style.height=window.screen.availHeight-275;

    (function() {
        superTable("GridView1", {
            cssSkin : "Default",
           fixedCols : 2,
           onFinish : function () 
              {             
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
                {
                   
                                this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                                {
                                   
                                    var dataRow = this.sDataTable.tBodies[0].rows[i];
                                    var fixedRow = this.sFDataTable.tBodies[0].rows[i];
            //                        var clicked =  fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked;
                                    return function () 
                                    {
                                        if (fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked) 
                                        {
                                            dataRow.style.backgroundColor = "#ffffff";
                                            fixedRow.style.backgroundColor = "#e4ecf7";
                                            fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
            //                                clicked = false;
                                        }
                                        else 
                                        {
                                            //被选中
                                            dataRow.style.backgroundColor = "#eeeeee";
                                            fixedRow.style.backgroundColor = "#adadad";
                                            fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
            //                                clicked = true;
                                            var odataRow;
                                            var ofixedRow;
                                            for(var m=0, n=fixedRow.parentNode.rows.length-1; m<n; m++)
                                            {
                                               odataRow=dataRow.parentNode.rows[m];
                                               ofixedRow=fixedRow.parentNode.rows[m];
//                                               if(m!=i)
//                                               {
//                                                 odataRow.style.backgroundColor = "#ffffff";
//                                                 ofixedRow.style.backgroundColor = "#e4ecf7";
//                                                 ofixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
//                                               }
                                            }
                                           
                                        }
                                    }
                                }.call(this, i);
                    
//                                this.sDataTable.tBodies[0].rows[i].ondblclick= this.sFDataTable.tBodies[0].rows[i].ondblclick = function (i) 
//                                {
//                                   var dataRow = this.sDataTable.tBodies[0].rows[i];
//                                   var fixedRow = this.sFDataTable.tBodies[0].rows[i];
//                                    return function () 
//                                    {
//                                       if(fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0]!=null)
//                                       {
//                                       
//                                           var date=new Date();
//                                           var time=date.getTime();
//                                           var id = fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
//                    
//                                           window.open("SM_WarehouseIN_WG.aspx?FLAG=READ&&ID=" + id+"&&tm="+time);
//                                       }
//                                    }
//                                }.call(this, i);
                            }
                         return this;
                   }
        });
    })();
    
ActivateAlertDiv('hidden', 'AlertDiv', '');
}
postBack=true;
 }
         
function ActivateAlertDiv(visstring, elem, msg){
     var adiv = $get(elem);
     adiv.style.visibility = visstring;                
}

//function order() {
//    var date=new Date();
//        var time=date.getTime();
//        window.open("SM_WarehouseIN_WGPush.aspx?FLAG=PUSH&id="+time);
//    }
function ToStorage() {
   var date=new Date();
       var time=date.getTime();
       window.open("SM_Warehouse_Query.aspx?FLAG=QUERY&id="+time);
   }

function projoverexport() {
     var date=new Date();
        var time=date.getTime();
        var retVal = window.showModalDialog("SM_Warehouse_ProjOver_Export.aspx?id="+time, "", "dialogWidth=900px;dialogHeight=600px;help=no;scroll=yes");
    }

     function changeEngid(tb)
        {
          var engid=tb.value
          tb.value=engid.split('-')[engid.split('-').length-1];
          tb.select();
        }

    
function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
   }
   
// function init()
//   {
//    window.moveTo(0,0);//移动窗口到0,0坐标
//    window.resizeTo(window.screen.availWidth,window.screen.availHeight);
//   }
//   document.onload=init();
    
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table width="98%">
                    <tr>
                        <td align="left">
                            <asp:CheckBox ID="CheckBoxShow" runat="server" AutoPostBack="true" Text="单据头完整显示"
                                OnCheckedChanged="CheckBoxShow_CheckedChanged" />
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Font-Size="Medium" Visible="false"></asp:Label>
                            <asp:HiddenField ID="hfdTotalNum" runat="server" />
                            <asp:HiddenField ID="hfdTotalAmount" runat="server" />
                        </td>
                        <td align="right">
                           <%-- <asp:Button ID="ButtonSearchUp" runat="server" Text="上查" OnClick="ButtonSearchUp_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ButtonSearchDown" runat="server" Text="下查" OnClick="ButtonSearchDown_Click" />&nbsp;&nbsp;&nbsp;--%>
                            <%--<input id="Display" type="button" value="隐藏条件" onclick="Disp()" />&nbsp;&nbsp;&nbsp;--%>
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />
                            &nbsp;&nbsp;&nbsp;
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="10">
                            </cc1:ModalPopupExtender>
                            <input id="InExport" type="button" value="导出" onclick="inexport()" runat="server"
                                visible="false" />
                            <asp:Button ID="BtnShowExport" runat="server" Text="导出" OnClick="BtnShowExport_Click" />
                            &nbsp;&nbsp;&nbsp;
                              <%--<input id="toorder" type="button" value="到订单" onclick="order()" runat="server" />&nbsp;&nbsp;&nbsp;--%>
                              <input id="toStorage" type="button" value="到库存" onclick="ToStorage()" runat="server" visible="false"/>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="98%" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="98%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td width="50%">
                                        <table cellpadding="4">
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    
                                                </td>
                                                <td>

                                                </td>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="50%" align="right">
                                        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="Query_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <table cellpadding="4">
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    生产制号：
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                               <asp:TextBox ID="TextBoxtaskid" runat="server" 
                                                                    OnTextChanged="TextBoxtaskid_TextChanged" AutoPostBack="true"          
                                                                   onclick="changeEngid(this);"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="ac1" runat="server" TargetControlID="TextBoxtaskid" FirstRowSelected="true"
                                                                    MinimumPrefixLength="1" ServiceMethod="getEngID" ServicePath="~/Ajax.asmx" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                    CompletionSetCount="30">
                                                                </cc1:AutoCompleteExtender>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    项目名称：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxprj" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    工程名称：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxeng" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                  
                                                    确认时间：
                                                </td>
                                                <td>
                                                     <asp:TextBox ID="TextBoxconfirmtime" runat="server"></asp:TextBox>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    计划跟踪号：
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="TextBoxptc" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    物料编码：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxmarterialid" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    物料名称：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxmarterialname" runat="server"></asp:TextBox>
                                                    
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    规格型号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxstandard" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    材质：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxattribute" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    国标：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxgb" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    计划模式：
                                                </td>
                                                <td>
                                                 <asp:TextBox ID="TextBoxpmode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    数量：
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="TextBoxnum" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="white-space: nowrap;" align="left">
                                                    仓库：
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="DropDownListwarehouse" OnSelectedIndexChanged="DropDownListwarehouse_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    仓位：
                                                </td>
                                                <td>
                                                   <asp:DropDownList ID="DropDownListposition" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    标识号：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxcgmode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left">
                                                    逻辑
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListFatherLogic" runat="server">
                                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="50%">
                                        <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                                            CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="1px" OnDataBound="GridViewSearch_DataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="逻辑">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)" Width="60px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                                            <asp:ListItem Value="OR" Selected="True">或者</asp:ListItem>
                                                            <asp:ListItem Value="AND">并且</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="名称">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)" Width="128px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListName" runat="server" Width="128px" Style="display: none">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="比较关系">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)" Width="80px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="7">不包含</asp:ListItem>
                                                            <asp:ListItem Value="8">左包含</asp:ListItem>
                                                            <asp:ListItem Value="9">右包含</asp:ListItem>
                                                            <asp:ListItem Value="1">等于</asp:ListItem>
                                                            <asp:ListItem Value="2">不等于</asp:ListItem>
                                                            <asp:ListItem Value="3">大于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="5">小于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于或等于</asp:ListItem>
                                                            
                                                            
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="数值">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBoxValue" runat="server" Width="128px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Panel ID="PanelWG" runat="server">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="False">
                            没有相关记录!</asp:Panel>
                        <table id="GridView1">
                            <asp:Repeater ID="RepeaterWG" runat="server" OnItemDataBound="RepeaterWG_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <td>  
                                          
                                        </td>
                                        <td>
                                            生产制号
                                        </td>
                                        <td>
                                            项目名称
                                        </td>
                                        <td>
                                          工程名称
                                        </td>
                                        <td>
                                            确认时间
                                        </td>
                                        <td>
                                            计划跟踪号
                                        </td>
                                        
                                        <td>
                                            物料代码
                                        </td>
                                        <td>
                                            物料名称
                                        </td>
                                        
                                        <td>
                                            型号规格
                                        </td>
                                        <td> 
                                          国标
                                        </td>
                                        <td>
                                            材质
                                        </td>
                                        <td>
                                            长
                                        </td>
                                        <td>
                                            宽
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            数量
                                        </td>
                                       <td>
                                           辅助数量
                                       </td>
                                       <td> 
                                         是否定尺
                                       </td>
                                        <td>
                                            仓库
                                        </td>
                                        <td>
                                            仓位
                                        </td>
                                        <td>
                                            批号
                                        </td>
                                                                               
                                        <td>
                                            订单编号
                                        </td>
                                       
                                        <td>
                                            计划模式
                                        </td>
                                        <td>
                                            标识号
                                        </td>
                                        <td>
                                            备注
                                        </td>
                                    </tr> 
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                         <asp:CheckBox runat="server" ID="CheckBox1" Visible="false" /> <%#Container.ItemIndex+1%>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelTaskID" runat="server" Text='<%#Eval("SQ_TASKID")%>'></asp:Label>
                                            <asp:Label ID="LabelSQCODE" runat="server" Text='<%#Eval("SQ_CODE")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPrj" runat="server" Text='<%#Eval("SQ_PRJ")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelEng" runat="server" Text='<%#Eval("SQ_ENG")%>'></asp:Label>
                                          
                                        </td>
                                         <td>
                                            <asp:Label ID="LabelConfirmtime" runat="server" Text='<%#Eval("SQ_CONFIRMTIME")%>'></asp:Label>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="LabelPTC" runat="server" Text='<%#Eval("PTC")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialCode")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MaterialStandard")%>
                                        </td>
                                        <td> 
                                          <%#Eval("GB") %>
                                        </td>
                                        <td>
                                            <%#Eval("Attribute")%>
                                        </td>
                                        <td>
                                            <%#Eval("Length")%>
                                        </td>
                                        <td>
                                            <%#Eval("Width")%>
                                        </td>
                                       <td>
                                            <%#Eval("Unit")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="Labelnum" runat="server" Text='<%#Eval("Num")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Labelfnum" runat="server" Text='<%#Eval("Fznum")%>'></asp:Label>
                                        </td>
                                        <td>
                                         <%#Eval("SQ_FIXED")%>
                                        </td>
                                        <td>
                                            <%#Eval("Warehousename")%>
                                        </td>
                                        <td>
                                            <%#Eval("Locationname")%>
                                        </td>
                                        <td>
                                            <%#Eval("Lotnum")%>
                                        </td>
                                       
                                     
                                        <td>
                                            <%#Eval("Orderid")%>
                                        </td>
                                       
                                        <td>
                                            <%#Eval("Pmode")%>
                                        </td>
                                        <td>
                                            <%#Eval("Cgmode")%>
                                        </td>
                                        <td>
                                            <%#Eval("Note")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                               
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td >
                                            总计:
                                        </td>
                                       <td>
                                       </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                           
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                       
                                        <td>
                                          <asp:Label ID="LabelSUMnum" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Labelsumfnum" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                       
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->

    <script type="text/javascript">
    
document.getElementById("GridView1").parentNode.className = "fakeContainer";
 document.getElementById("GridView1").parentNode.style.width=window.screen.availWidth-182;
document.getElementById("GridView1").parentNode.style.height=window.screen.availHeight-275;
(function() {
    superTable("GridView1", {
        cssSkin : "Default",
       fixedCols : 2,
       onFinish : function () 
       {             
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
                {
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                       
                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
//                        var clicked =  fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked;
                        return function () 
                        {
                            if (fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked) 
                            {
                                dataRow.style.backgroundColor = "#ffffff";
                                fixedRow.style.backgroundColor = "#e4ecf7";
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
//                                clicked = false;
                            }
                            else 
                            {
                                //被选中
                                dataRow.style.backgroundColor = "#eeeeee";
                                fixedRow.style.backgroundColor = "#adadad";
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
//                                clicked = true;
                                var odataRow;
                                var ofixedRow;
                                for(var m=0, n=fixedRow.parentNode.rows.length-1; m<n; m++)
                                {
                                   odataRow=dataRow.parentNode.rows[m];
                                   ofixedRow=fixedRow.parentNode.rows[m];
//                                   if(m!=i)
//                                   {
//                                     odataRow.style.backgroundColor = "#ffffff";
//                                     ofixedRow.style.backgroundColor = "#e4ecf7";
//                                     ofixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
//                                   }
                                }
                               
                            }
                        }
                    }.call(this, i);
                    
                   
                    
//                         this.sDataTable.tBodies[0].rows[i].ondblclick= this.sFDataTable.tBodies[0].rows[i].ondblclick = function (i) 
//                        {
//                            var dataRow = this.sDataTable.tBodies[0].rows[i];
//                            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
//                            
//                            return function () 
//                            {
//                                if(fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0]!=null)
//                                { 
//                                
//                                    var date=new Date();
//                                    
//                                    var time=date.getTime();
//                                    
//                                    var id = fixedRow.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
//            
//                                    window.open("SM_WarehouseIN_WG.aspx?FLAG=READ&&ID=" + id+"&&tm="+time);
//                                    
//                                }
//                            }
//                        }.call(this, i);
                }
             return this;
       }
    });
})();

    </script>

    <div id="AlertDiv" class="AlertStyle">
        <img id="laoding" src="../Assets/images/ajaxloader.gif" alt="downloading" />
    </div>
</asp:Content>

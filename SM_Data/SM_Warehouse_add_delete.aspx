<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true"
    CodeBehind="SM_Warehouse_add_delete.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_add_delete" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
                <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

                <script src="SM_JS/superTables.js" type="text/javascript"></script>

                <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

   
  <script type="text/javascript" language="javascript">
 var postBack=true;
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args){
 if (args.get_postBackElement().id == "<%= btnClose.ClientID %>"||args.get_postBackElement().id == "<%= btnReset.ClientID %>")
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
if(document.getElementById("<%=GridViewShow.ClientID %>") == null){
                 
        ActivateAlertDiv('hidden', 'AlertDiv', '');
      
}

else{

   document.getElementById("<%=GridViewShow.ClientID %>").parentNode.className = "fakeContainer";  
   document.getElementById("<%=GridViewShow.ClientID %>").parentNode.style.height=window.screen.availHeight-230; 

    (function() {
        superTable("<%=GridViewShow.ClientID %>", {
            cssSkin : "Default",
           fixedCols : 2,
           onFinish : function () 
              {             
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length; i<j; i++) 
                {
                   
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                        var clicked = false;

                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                        return function () 
                              {
                                    if (clicked) 
                                    {
                                        dataRow.style.backgroundColor = "#ffffff";
                                        fixedRow.style.backgroundColor = "#e4ecf7";
                                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                        clicked = false;
                                    }
                                    else 
                                    {
                                        dataRow.style.backgroundColor = "#BFDFFF";
                                        fixedRow.style.backgroundColor = "#409FFF";
                                        fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                        clicked = true;
                                    }
                                }
                                }.call(this, i);
                            }
                         return this;
                   }
        });
    })();
    
ActivateAlertDiv('hidden', 'AlertDiv', '');
}
}
postBack=true;
 }
         
function ActivateAlertDiv(visstring, elem, msg){
    var adiv = $get(elem);
     adiv.style.visibility = visstring;                
}
  function warnexport() {
       var date=new Date();
        var time=date.getTime();
        var retVal = window.showModalDialog("SM_Warehouse_Warn_Export.aspx?action=bk&id="+time, "", "dialogWidth=900px;dialogHeight=600px;help=no;scroll=yes");
      }
 
   function viewCondition(){
           document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
        }
 
                </script>

                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table width="100%">
                    <tr>
                        <td align="left" style="width: 50%">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                X="10" Y="10">
                            </cc1:ModalPopupExtender>
                            &nbsp;&nbsp;
                            <input id="WarnExport" type="button" value="导出" onclick="warnexport()" runat="server" />&nbsp;&nbsp;
                            <asp:Button ID="Add" runat="server" Text="添加" OnClick="Add_Click" />&nbsp;&nbsp;
                            <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="100%" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td style="white-space: nowrap;" align="left">
                                        &nbsp;
                                    </td>
                                    <td style="white-space: nowrap;" align="left">
                                        <asp:Button ID="btnQuery" runat="server" OnClick="Query_Click" Text="查询" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;物&nbsp;料&nbsp;代&nbsp;码：<asp:TextBox ID="TextBoxMCode" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;物&nbsp;料&nbsp;名&nbsp;称：<asp:TextBox ID="TextBoxMName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;规&nbsp;格&nbsp;型&nbsp;号：<asp:TextBox ID="TextBoxMStandard" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;材&nbsp;&nbsp;&nbsp;质：<asp:TextBox
                                                        ID="TextBoxCZ" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;国&nbsp;&nbsp;&nbsp;标：<asp:TextBox
                                                        ID="TextBoxGB" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp;&nbsp;&nbsp;安全库存数：<asp:TextBox ID="TextBoxWarnNum" runat="server"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;类&nbsp;&nbsp;&nbsp;别：
                                                    <asp:DropDownList ID="DropDownListType" runat="server" Width="90px">
                                                            <asp:ListItem Text="—请选择—" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="钢材类" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="焊材类" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="耗材类" Value="3"></asp:ListItem>
                                                             <asp:ListItem Text="油漆类" Value="4"></asp:ListItem>
                                                              <asp:ListItem Text="采购成品类" Value="5"></asp:ListItem>
                                                        </asp:DropDownList>
                                                </td>
                                                <td style="white-space: nowrap;" align="left" width="50%">
                                                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 逻&nbsp;&nbsp;&nbsp;辑：
                                                    <asp:DropDownList ID="DropDownListFatherLogic" runat="server">
                                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
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
             <%-- <Triggers>
           <asp:PostBackTrigger  ControlID="btnQuery" />
          </Triggers>--%>
                <ContentTemplate >
                
                    <asp:Panel ID="PanelBody" runat="server">
                 
                        <asp:GridView ID="GridViewShow" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="MARID">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="物料编码" DataField="MARID"></asp:BoundField>
                                <asp:BoundField HeaderText="物料名称" DataField="MNAME"></asp:BoundField>
                                <asp:BoundField HeaderText="规格型号" DataField="GUIGE"></asp:BoundField>
                                <asp:BoundField HeaderText="材质" DataField="CAIZHI"></asp:BoundField>
                                <asp:BoundField HeaderText="国标" DataField="GB"></asp:BoundField>
                                <asp:BoundField HeaderText="合理库存量" DataField="REASONABLENUM"></asp:BoundField>
                                <asp:BoundField HeaderText="报警数量" DataField="WARNNUM"></asp:BoundField>
                                <asp:BoundField HeaderText="单位" DataField="PURCUNIT"></asp:BoundField>
                              <asp:TemplateField HeaderText="类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                     <label runat="server" id="lblType"><%# Eval("Type").ToString() == "1" ? "钢材类" : Eval("Type").ToString() == "2" ? "焊材类" : Eval("Type").ToString() == "3" ? "耗材类" : Eval("Type").ToString() == "4" ? "油漆类" : Eval("Type").ToString() == "5" ? "采购成品类" : ""%></label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="是否班组结算物料" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                     <label runat="server" id="lblzjsbz"><%# Eval("BZJSBZ").ToString() == "1" ? "是" : "否"%></label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Change" CssClass="link" NavigateUrl='<%#"SM_Warehouse_add_detail.aspx?flag=modify&&mar_id="+Eval("MARID")%>'
                                            runat="server">
                                            <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />修改
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                           
                        </asp:GridView>
                        
                        
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="False" ForeColor="Red">
                            没有相关的物料!</asp:Panel>
                    </asp:Panel>

                    <script type="text/javascript">
                            
         document.getElementById("<%=GridViewShow.ClientID %>").parentNode.className = "fakeContainer";           
     
         document.getElementById("<%=GridViewShow.ClientID %>").parentNode.style.height=window.screen.availHeight-230;         

(function() {
    superTable("<%=GridViewShow.ClientID %>", {
        cssSkin : "Default",
        fixedCols : 2,
        onFinish : function () 
        {  
                for (var i=0, j=this.sDataTable.tBodies[0].rows.length; i<j; i++) 
                {
                    
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                        var clicked = false;
                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];

                        return function () 
                        {
                            if (clicked) 
                            {
                              
                                dataRow.style.backgroundColor = "#ffffff";
                                fixedRow.style.backgroundColor = "#e4ecf7";
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                clicked = false;
                             
                            }
                            else 
                            {
                              if( fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
                              {
                                dataRow.style.backgroundColor = "#BFDFFF";
                                fixedRow.style.backgroundColor = "#409FFF";
                                 
                                fixedRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                clicked = true;
                              }
                            }
                        }
                    }.call(this, i);
                }
             return this;
       }
    });
})();
                    </script>

                </ContentTemplate>
            </asp:UpdatePanel>
            
             <div id="AlertDiv" class="AlertStyle">
                <img id="laoding" src="../Assets/images/ajaxloader.gif" alt="downloading" />
            </div>
        </div>
    </div>
</asp:Content>

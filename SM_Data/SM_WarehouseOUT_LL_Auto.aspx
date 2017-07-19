<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseOUT_LL_Auto.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOUT_LL_Auto"
    Title="重机领料单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

function allsel()
  {
   var  table=document.getElementById('<%= GridView1.ClientID %>');
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
     if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
       }
    }
  }

function cancelsel()
{
   var  table=document.getElementById('<%= GridView1.ClientID %>');
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
       if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
       }
    }
}

function consel()
{
    table=document.getElementById('<%= GridView1.ClientID %>');
    tr=table.getElementsByTagName("tr");//这里的tr是一个数组
    for(var i=1;i<(tr.length-1);i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
        {
            if(obj.checked)
            {
                obj.checked=true;

                for(var j=i+1;j<tr.length;j++)
                {
                    var nextobj=tr[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    if(nextobj!=null)
                    {
                    
                        if(nextobj.type.toLowerCase()=="checkbox" && nextobj.value!="")
                        {
                            if(nextobj.checked)
                            {
                                for(var k=i+1;k<j+1;k++)
                                {
                                    tr[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;

                                }
                            }
                        }  
                    }
                }
            }
        }
    }
}

  function checkRN(tb) {
            var rn = parseFloat(tb.value);
            var par = tb.parentNode.parentNode;
            var dn = parseFloat(par.getElementsByTagName("td")[12].getElementsByTagName("span")[0].innerHTML);
            if (isNaN(dn)) {
                dn=0;
            }
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            if (isNaN(rn)) {
                alert("请输入正确的数量！");
                tb.value = dn;
            }
            if ((color == '0') && (rn > dn)) {
                alert("出库数量不能大于库存数量！");
                tb.value = dn;
            }
            if (rn == 0) {
                tb.value = dn;
            }
            if ((color == '0') && (rn < 0)) {
                tb.value = -rn;
                if (tb.value > dn) {
                    tb.value = dn;
                }
             
            }
        }
        
            function checkRQN(tb) {
            var rqn = parseFloat(tb.value);
            var par = tb.parentNode.parentNode;
            var dqn = Math.round(par.getElementsByTagName("td")[14].getElementsByTagName("span")[0].innerHTML);
             if (isNaN(dqn)) {
                dqn=0;
            }
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            if (isNaN(rqn)) {
                alert("请输入正确的数量！");
                tb.value = dqn;
                
            }
            if ((color == '0') && (rqn > dqn)) {
                alert("出库数量不能大于库存数量！");                
                tb.value =dqn;
                
            }            
            if ((color == '0') && (rqn < 0)) {
                tb.value = -rqn;
                if (tb.value > dqn) {
                    tb.value = dqn;
                }
               
            }
        }

function ClientSideClick(myButton) {
     
           // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }
            
            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // diable the button
                myButton.disabled = true;
                myButton.value = "加载中...";              
            }
            
           return true;
        }
        function changeEngid(tb)
        {
          var engid=tb.value
          tb.value=engid.split('-')[engid.split('-').length-1];
          tb.select();
        }
        
       function ac2_ItemSelected(sender,e)
       {
            var  ac2Value=document.getElementById('<%= ac2Value.ClientID %>');
            ac2Value.value=e.get_value();
       } 
       
       function ClientSideClick(myButton) {
     
           // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }
            
            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // diable the button
                myButton.disabled = true;
                myButton.value = "加载中...";              
            }
            
           return true;
        }
        
          function SearchMar()
         {
            var date=new Date();
            var time=date.getTime();
          
            window.showModalDialog("SM_Warehouse_Query.aspx?FLAG=QUERY&&id="+time, '', "dialogHeight: 600px; dialogWidth: 1200px; center: Yes; help: No; resizable: No; status: No;");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="0">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="DeleteItem" runat="server" Text="删除条目" OnClick="DeleteItem_Click"
                                OnClientClick=" return confirm('您确认删除此条目吗?');" />
                            <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" OnClientClick="ClientSideClick(this);"
                                UseSubmitBehavior="False" />
                            <asp:Button ID="Verify" runat="server" Text="审核" OnClick="Verify_Click" OnClientClick="ClientSideClick(this);"
                                UseSubmitBehavior="False" />
                            <asp:Button ID="DeleteFrom" runat="server" Text="删除单据" OnClick="DeleteFrom_Click"
                                OnClientClick=" return confirm('您确认删除此单据吗?');" />
                        </td>
                        <td align="right">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="HeadPanel" runat="server" Width="98%" Style="overflow: auto; position: static">
                <table width="100%">
                    <tr>
                        <td>
                            系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="font-size: x-large; text-align: center;" colspan="2">
                            重机领料单<asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="false" />
                        </td>
                        <td>
                            <asp:Image ID="ImageRed" runat="server" ImageUrl="~/Assets/images/red.gif" Visible="false" />
                            &nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号&nbsp;&nbsp;&nbsp;：
                            <asp:Label ID="LabelCode" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                            <input type="text" id="InputColour" style="display: none" runat="server" />
                            <asp:Label ID="LabelBillType" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;日期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate"
                                Format="yyyy-MM-dd">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    &nbsp;&nbsp;&nbsp;任务号：<asp:TextBox ID="TextBoxSCZH" runat="server" Width="170px"
                                         AutoPostBack="true" onblur="getEngid(this)";"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="ac1" runat="server" TargetControlID="TextBoxSCZH" FirstRowSelected="true"
                                        MinimumPrefixLength="1" ServiceMethod="getTaskID" ServicePath="~/Ajax.asmx" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionSetCount="30">
                                    </cc1:AutoCompleteExtender>
                                    <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="TextBoxSCZH"
                                        PopupControlID="Panel1" OffsetX="0" OffsetY="-18">
                                    </cc1:HoverMenuExtender>
                                    <asp:Panel ID="Panel1" runat="server" Style="background-color: #79c8ef; visibility: hidden;">
                                        <asp:Label ID="LabelSCZH" runat="server" ForeColor="Black"></asp:Label>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            领料部门：<asp:DropDownList ID="DropDownListDep" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;领料班组:
                            <asp:DropDownList ID="DropDownListBZ" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextBoxComment" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="CheckBoxDate" runat="server" Text="会计期间" CssClass="checkBoxCss" />
                        </td>
                        <%--<td>
                            &nbsp;&nbsp;&nbsp;子项名称：
                            <asp:DropDownList ID="DropDownListZXMC" runat="server">
                                <asp:ListItem Value="--">-请选择-</asp:ListItem>
                                <asp:ListItem Value="包装">--包装--</asp:ListItem>
                                <asp:ListItem Value="支撑">--支撑--</asp:ListItem>
                                <asp:ListItem Value="窑托">--窑托--</asp:ListItem>
                                <asp:ListItem Value="磨托">--磨托--</asp:ListItem>
                                <asp:ListItem Value="备料">--备料--</asp:ListItem>
                                <asp:ListItem Value="底座">--底座--</asp:ListItem>
                                <asp:ListItem Value="预组装">--预组装--</asp:ListItem>
                                
                            </asp:DropDownList>
                        </td>--%>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;张数:<asp:TextBox ID="TextBoxPageNum" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    
                    <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注:
                            
                            <asp:TextBox ID="TextBoxNOTE1" runat="server"></asp:TextBox>
                        </td>
                    <td colspan="3"></td>
                    </tr>     
                </table>
            </asp:Panel>
            <div>
                &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                    id="continue" type="button" value="连选" onclick="consel()" />
                &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                    <asp:Image ID="InfoImage" ImageUrl="~/Assets/images/read.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" onclick="SearchMar();"/>
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelBody" runat="server" Width="100%" Height="300px" ScrollBars="Auto">
                        <asp:HiddenField ID="ac2Value" runat="server" />
                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                        <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UniqueID")%>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="MaterialCode" HeaderText="物料代码" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelSQCODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SQCODE")%>'
                                            Visible="false"></asp:Label>
                                        <asp:TextBox ID="TextBoxMaterialCode" runat="server" BorderStyle="None" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>'
                                            Width="90px" OnTextChanged="TextBoxMaterialCode_TextChanged" AutoPostBack="true"
                                            onclick="this.select();"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="ac2" runat="server" CompletionSetCount="10" DelimiterCharacters=""
                                            Enabled="True" MinimumPrefixLength="1" ServiceMethod="StorgeCode" CompletionListCssClass="autocomplete_completionListElement"
                                            OnClientItemSelected="ac2_ItemSelected" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ServicePath="~/Ajax.asmx"
                                            FirstRowSelected="true" TargetControlID="TextBoxMaterialCode" UseContextKey="True">
                                        </cc1:AutoCompleteExtender>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        合计:
                                    </FooterTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="MaterialName" HeaderText="物料名称" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="MaterialStandard" HeaderText="规格型号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Attribute" HeaderText="材质" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="GB" HeaderText="国标" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="LotNumber" HeaderText="批号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="长(mm)" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="宽(mm)" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="即时库存数量" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelDN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DN")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="LabelTotalDN" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Wrap="false" HorizontalAlign="Right" />
                                    <FooterStyle Wrap="false" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="实发数量" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBoxRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>'
                                            onblur="checkRN(this)" Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="LabelTotalRN" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="即时库存张(支)" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelDQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DQN")%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="LabelTotalDQN" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Wrap="false" HorizontalAlign="Right" />
                                    <FooterStyle Wrap="false" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="实发张(支)" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBoxRQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RQN")%>'
                                            onblur="checkRQN(this)" Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="LabelTotalRQN" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="计划模式" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="PTC" HeaderText="计划跟踪号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'
                                            Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelOrderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderCode")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="发料仓库" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="LabelPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="标识号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelBSH" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BSH")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBoxComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                            <RowStyle BackColor="#EFF3FB" Wrap="false" />
                            <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <EditRowStyle BackColor="#2461BF" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel ID="FooterPanel" runat="server" Width="98%" Style="overflow: auto; position: static">
                <table width="100%">
                    <tr>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                            <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;发&nbsp;&nbsp;&nbsp;料：<asp:DropDownList ID="DropDownListSender"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server" Width="100px"></asp:Label>
                            <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left" style="width: 25%;">
                            审核日期：<asp:Label ID="LabelApproveDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>

    <script type="text/javascript">
  
   var sDataTable=document.getElementById("<%=GridView1.ClientID %>")
  
   function RowClick()
   {
           for (var i=1, j=sDataTable.tBodies[0].rows.length-1; i<j; i++) 
          {
            sDataTable.tBodies[0].rows[i].onclick= function (i) 
            {
                
                var dataRow = sDataTable.tBodies[0].rows[i];
                return function () 
                      {
                            if (dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked) 
                            {
                                dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                dataRow.style.backgroundColor = "#EFF3FB";
                            }
                            else 
                            {
                                dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                dataRow.style.backgroundColor = "#D1DDF1";
                            }
                        }
             }.call(this, i);
           }
   }

   RowClick();

    </script>

</asp:Content>

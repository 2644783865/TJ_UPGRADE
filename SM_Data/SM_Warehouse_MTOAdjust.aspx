<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true"
    CodeBehind="SM_Warehouse_MTOAdjust.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_MTOAdjust"
    Title="MTO调整" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
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
        function checkNum(tb) {
            var ajdn = parseFloat(tb.value);
            var wn = parseFloat(tb.parentNode.parentNode.getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML);

            if (isNaN(ajdn)) {
                alert("请输入正确的数量！");
                tb.value = wn;
                Statistic();
            }
            if (ajdn <= 0 || ajdn > wn) {
                alert("调整数量必须大于0且不大于可调数量！");
                tb.value = wn;
                Statistic();
            }
            Statistic();
        }

        function checkQuantity(tb) {
            var adjqn = parseFloat(tb.value);
            var wqn = parseFloat(tb.parentNode.parentNode.getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML);

            if (isNaN(adjqn)) {
                alert("请输入正确的数量！");
                tb.value = wqn;
                Statistic();
            }
            if (adjqn < 0 || adjqn > wqn) {
                alert("调整数量必须大于0且不大于可调数量！");
                tb.value = wqn;
                Statistic();
            }
            Statistic();
        }
        
        function Statistic() {
            var tadjn = 0;
            var tadjqn = 0;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            
            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var val1 = gv1.rows[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value;
                var val2 = gv1.rows[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
                tadjn += parseFloat(val1);
                tadjqn += Math.round(val2);
            }
            var lbtn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[19].getElementsByTagName("span")[0];
            lbtn.innerHTML = tadjn.toFixed(4);
            var lbtqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[20].getElementsByTagName("span")[0];
            lbtqn.innerHTML = tadjqn;
        }

        function append() {
            var retVal = window.showModalDialog("SM_Warehouse_Query.aspx?FLAG=APPENDMTO","","dialogWidth=1280px;dialogHeight=800px;status=no;help=no;scroll=yes");
            return retVal;
        }

        function closewin() {
            window.opener.location = window.opener.location.href;
            window.close();
        }
        
        function btnPrint_onclick() {
        window.showModalDialog('SM_Warehouse_MTO_Print.aspx?mtocode=<%=LabelCode.Text %>','',"dialogWidth=900px;dialogHeight=600px;scroll:no");   
        }
        
         /*填充仓位*/
        function ChoosePosition(tb)
         {
          
          var date=new Date();
           var time=date.getTime();
            var par = tb.parentNode.parentNode;
            var wscode=par.getElementsByTagName("td")[12].getElementsByTagName("span")[1].innerHTML;
            var val = new Array(2);
            val = window.showModalDialog("SM_WarehouseIN_PositionChoice.aspx?WSCODE=" + wscode+"&&id="+time, '', "dialogHeight:550px; dialogWidth: 800px; center: Yes; help: No; resizable: No; status: No;");
            if (val != null) 
            {
                tb.value = val[1];
                var par = tb.parentNode.parentNode;
                var positioncode = par.getElementsByTagName("td")[13].getElementsByTagName("input")[1];
                positioncode.value = val[0];
            }
        }
        
        
           function allsel()
  {
   var  table=document.getElementById("ctl00_PrimaryContent_GridView1");
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
     if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
//        tr[i].style.backgroundColor ='#55DF55'; 
        }
    }
  }

function cancelsel()
{
   var  table=document.getElementById("ctl00_PrimaryContent_GridView1");
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
       if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
//        tr[i].style.backgroundColor='#EFF3FB'; 
        }
    }
}

function consel()
{
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr");//这里的tr是一个数组
    for(var i=1;i<(tr.length-1);i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
        {
            if(obj.checked)
            {
                obj.checked=true;
//                obj.parentNode.parentNode.style.backgroundColor ='#55DF55'; 
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
//                                    tr[k].style.backgroundColor ='#55DF55'; 
                                }
                            }
                        }  
                    }
                }
            }
        }
    }
}
         function viewCondition(){
   document.getElementById("<%=PanelSplitLineHandle.ClientID%>").style.display='block';
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
                myButton.value = "审核中...";              
            }
            
           return true;
        }
        
         function tostorage() {
     var date=new Date();
        var time=date.getTime();
        window.open("SM_Warehouse_Query.aspx?FLAG=PUSHMTO&id="+time,'_self');
    }
        
        
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Panel ID="Operation" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <input id="Close" type="button" value="关闭" onclick="closewin()" />&nbsp;|
                                <asp:Button ID="Append" runat="server" Text="追加" CausesValidation="false" OnClick="Append_Click"
                                    OnClientClick="return append()" />&nbsp;|
                             <asp:Button ID="Split" runat="server" Text="拆分" OnClientClick="viewCondition()" />&nbsp;|
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSplit" runat="server" TargetControlID="Split"
                                PopupControlID="PanelSplitLine" Drag="True" PopupDragHandleControlID="PanelSplitLineHandle"
                                Enabled="True" X="500" Y="300" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                                <asp:Button ID="Delete" runat="server" Text="删除" CausesValidation="false" OnClick="Delete_Click" />&nbsp;|
                                <asp:Button ID="HB" runat="server" Text="合并" OnClick="HB_Click" OnClientClick="return confirm('确定要合并该MTO单？');" /> &nbsp;|                               
                                <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" CausesValidation="false" />&nbsp;|
                                <asp:Button ID="CD" runat="server" Text="拆单" OnClick="CD_Click" OnClientClick="return confirm('确定要拆分该入库单？');" />&nbsp;|                                                               
                                <asp:Button ID="Verifiy" runat="server" Text="审核" OnClick="Verify_Click" OnClientClick="ClientSideClick(this);" UseSubmitBehavior="False"  />&nbsp;|
                                <asp:Button ID="AntiVerify" runat="server" Text="反审" OnClick="AntiVerify_Click" />&nbsp;|
                                <asp:Button ID="DeleteBill" runat="server" Text="删单" OnClick="DeleteBill_Click" />&nbsp;|
                                <asp:Button ID="Related" runat="server" Text="关联单据" OnClick="Related_Click" />&nbsp;|
                                <input id="btnstorge" type="button" value="到库存" onclick="tostorage()" runat="server" />&nbsp;|
                                <input id="btnPrint" runat="server" type="button" value="打印" onclick="return btnPrint_onclick()" visible="false" />&nbsp;|
                            <td align="right">
                                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
    <asp:Panel ID="PanelSplitLineHandle" runat="server" Style="display: none">
        <asp:Panel ID="PanelSplitLine" runat="server">
            <table style="background-color: #CCCCFF; border: solid 1px black;">
                <tr>
                    <td align="center" colspan="2">
                        条目拆分选项
                    </td>
                </tr>
                <tr>
                    <td>
                        记录拆分模式：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonListSplitMode" runat="server" RepeatColumns="2"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="均分" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="复制" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        记录复制模式：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonListSplitMode2" runat="server" RepeatColumns="2"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="插入" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="追加" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        记录拆分行数：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxSplitLineNum" runat="server" Text="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="ButtonSplitOK" runat="server" OnClick="ButtonSplitOK_Click" Text="确定"
                            />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="ButtonSplitCancel" runat="server" OnClick="ButtonSplitCancel_Click"
                            Text="取消" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="HeadPanel" runat="server" Width="100%">
                <table width="100%">
                   <tr>            
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="从计划跟踪号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="物料代码" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="物料名称" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox4" runat="server" Text="规格型号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="材质" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox6" runat="server" Text="国标" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox7" runat="server" Text="批号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox8" runat="server" Text="是否定尺" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox9" runat="server" Text="长" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox10" runat="server" Text="宽" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox11" runat="server" Text="可调张(支)数" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox12" runat="server" Text="调整张(支)数" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" /> 
                        <asp:CheckBox ID="CheckBox13" runat="server" Text="订单单号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />                      
                        <asp:CheckBox ID="CheckBox14" runat="server" Text="标准件" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
          
                   </tr> 
                    <tr>
                        <td>
                            系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="font-size: x-large; text-align: center;" colspan="3">
                            MTO调整单<asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td style="width: 20%">
                            编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server" Enabled="false"></asp:Label>
                            <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            制单日期：<asp:TextBox ID="TextBoxDate" runat="server" Enabled="false"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate"
                                Format="yyyy-MM-dd">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 40%">
                            <asp:TextBox ID="TextBoxPTCTo" runat="server" Width="170px" OnTextChanged="TextBoxPTCTo_TextChanged"
                                AutoPostBack="true" onclick="this.select();" Visible="false"></asp:TextBox>
                            仓库:<asp:DropDownList ID="DropDownListWarehouse" runat="server" Width="80px" AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownListWarehouse_SelecedIndexChanged">
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                            仓位:<asp:DropDownList ID="DropDownListPosition" runat="server" Width="80px" AutoPostBack="True"
                                AppendDataBoundItems="true" OnSelectedIndexChanged="DropDownListPosition_SelecedIndexChanged">
                                <asp:ListItem Value="0">待查</asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownListPTCTo" runat="server" Width="80px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPTCTo_SelecedIndexChanged">
                              <asp:ListItem Value="0">--请选择--</asp:ListItem>
                               <asp:ListItem Value="1">备库</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%">
                            备注：<asp:TextBox ID="TextBoxComment" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div>
                &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                    id="continue" type="button" value="连选" onclick="consel()" />
                &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
            </div>
            <asp:Panel ID="PanelBody" runat="server" Style="overflow: auto; position: relative;
                margin: 2px" Width="100%" Height="400px">
                <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    EmptyDataText="没有相应的数据！" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UniqueID")%>'
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料代码" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelSQCODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SQCODE")%>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                合计：</FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="规格型号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="长" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="宽" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="从计划跟踪号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPTCFrom" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTCFrom")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓库" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                                <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                    Style="display: none"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'
                                    Width="60px" onclick="ChoosePosition(this)"></asp:TextBox>
                                <input type="text" id="InputPositionCode" value='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                    style="display: none" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="可调数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelWN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WN")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalWN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="可调张(支)数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelWQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WQN")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalWQN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="到计划跟踪号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxGVPTCTo" runat="server" onclick="this.select();if(this.value=='--请选择--'){this.value='';}" Text='<%#DataBinder.Eval(Container.DataItem, "PTCTo")%>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="计划模式" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="调整数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxAdjN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AdjN")%>'
                                    onblur="checkNum(this)" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalAdjN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="调整张(支)数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxAdjQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AdjQN")%>'
                                    onblur="checkQuantity(this)" Width="80px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalAdjQN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxNote" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Note")%>'
                                    Width="100px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                    <RowStyle BackColor="#EFF3FB" Wrap="false" />
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" Wrap="false" />
                    <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />
                </yyc:SmartGridView>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="FooterPanel" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td align="left" style="width: 25%;">
                                    &nbsp;&nbsp;&nbsp;技术员：<asp:DropDownList ID="DropDownListPlaner" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 25%;">
                                    &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：
                                    <asp:DropDownList ID="DropDownListDep" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListDep_SelectedIndexChanged">
                                        <asp:ListItem Value="03">技术部</asp:ListItem>
                                        <asp:ListItem Value="09">电气制造部</asp:ListItem>
                                        <asp:ListItem Value="07">储运部</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 25%;">
                                    &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                                    <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 25%;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25%;">
                                    &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server"></asp:Label>
                                    <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 25%;">
                                    审核日期：<asp:Label ID="LabelApproveDate" runat="server"></asp:Label>
                                </td>
                                <td align="left" style="width: 25%;">
                                </td>
                                <td align="left" style="width: 25%;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
           <script type="text/javascript">  
               var sDataTable=document.getElementById("<%=GridView1.ClientID %>")  
              
               function RowClick()//点击某一行选中
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
                                            dataRow.style.backgroundColor = "#adadad";
                                           
            //                              var odataRow;
            //                                  for (var m=1, n=sDataTable.tBodies[0].rows.length-1; m<n; m++) 
            //                                  {
            //                                     odataRow=sDataTable.tBodies[0].rows[m];
            //                                     if(odataRow!=dataRow)
            //                                     {
            //                                        odataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
            //                                        odataRow.style.backgroundColor = "#EFF3FB";
            //                                     }
            //                                  }
                                        }
                                    }
                         }.call(this, i);
                       }
               }

               RowClick();

            </script>
        </div>
    </div>
</asp:Content>

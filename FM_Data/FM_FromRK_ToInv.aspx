<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="FM_FromRK_ToInv.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_FromRK_ToInv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    从入库单下推发票

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
// <!CDATA[

//从入库到发票
function ConfirmRK(){
 var row=document.getElementById('<%=grvRK.ClientID %>').rows.length;
 var count=0;
 for(var i=2;i<row;i++)
  {
    var t="ctl00_PrimaryContent_grvRK_ctl";
    if(i<10)
    {
      t+="0"+i+"_ckbRKBH";
    }
    else
    {
     t+=i+"_ckbRKBH";
    }
   var obj=document.getElementById(t);
   if(obj.checked)
   {
      count=count+1;
   }
 }

 if(count==0)
 {
   alert("请选择要下推发票的入库单！");
   return false;
 }
 else
 {
   return confirm("共选择了"+count+"条入库记录！\r\r确认下推发票吗？");
 }
}

//入库单详细
function ShowDetail(obj)
{
  var wg_code=obj.title;
  var time=new Date();
  var sec=time.getTime();
  window.showModalDialog("FM_Create_WareHouseInvDetail.aspx?wgcode="+wg_code+"&nouse="+sec,'',"dialogWidth=1000px;dialogHeight=580px;status=no;help=no;");
}

function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}

function allsel()
  {
   var  table=document.getElementById("<%=grvRK.ClientID%>");
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
   var  table=document.getElementById("<%=grvRK.ClientID%>");
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
    var  table=document.getElementById("<%=grvRK.ClientID%>");
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


// ]]>
    </script>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="RightContent">
       <asp:HiddenField ID="hfdTotalNum" runat="server" />
       <asp:HiddenField ID="hfdTotalAmount" runat="server" />
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td style="width: 20%;">
                                <input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                                 id="continue" type="button" value="连选" onclick="consel()" />
                               <input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />             
                            </td>
                            <td align="right">
                                <asp:HyperLink ID="hledit" runat="server" CssClass="hand">
                                    <asp:Image ID="Image4" ImageUrl="~/Assets/icons/move_arrange.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" OnClick="viewCondition()" />筛选条件</asp:HyperLink>
                                <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                    Position="Bottom" Enabled="true" runat="server" OffsetX="-600" OffsetY="-30" TargetControlID="hledit"
                                    PopupControlID="PanelCondition" EnableViewState="true">
                                </asp:PopupControlExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnXTFP" runat="server" Text="下推发票" OnClick="btnXTFP_Click"  />
<%--                            下推发票js    OnClientClick="javascript:return ConfirmRK();"
--%>                            </td>
                            <td align="right" style="width: 10%">
                                <a href="FM_Invoice_Managemnt.aspx" title="返回到 发票管理界面">
                                    返回</a> &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="500px" Style="display: none">
                        <table width="500px" style="background-color: #CCCCFF; border: solid 1px black;">
                            <tr>
                                <td colspan="4" align="right">
                               
                                    
                                    <asp:Button ID="btnsearch" runat="server" Text="查 询" OnClick="btnQuery_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                     &nbsp;&nbsp;&nbsp;
                                    <input id="ipClose" runat="server" type="button" onclick="document.body.click(); return false;"
                                        value="取消" />
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="100px">
                                    起始时间：
                                </td>
                                <td align="left">
                                    <asp:TextBox id="starttime0" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                            TargetControlID="starttime0">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right" width="100px">
                                    结束时间：
                                </td>
                                <td align="left">
                                    <asp:TextBox id="finishtime0" runat="server" type="text"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                            TargetControlID="finishtime0">
                                        </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                                        CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px" OnDataBound="GridViewSearch_DataBound">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="名称">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)" Width="128px"></asp:TextBox>
                                                    <asp:DropDownList ID="DropDownListName" runat="server" Width="128px" Style="display: none">
                                                        <asp:ListItem Value="NO" Selected="True" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="WG_CODE">入库单编号</asp:ListItem>
                                                        <asp:ListItem Value="WG_COMPANY">供应商</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="比较关系">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)" Width="80px"></asp:TextBox>
                                                    <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
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
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
          
                <yyc:SmartGridView ID="grvRK" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                   ShowFooter="true" CellPadding="4" ForeColor="#333333" OnRowDataBound="grvRK_RowDataBound">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server" Checked="false" Onclick="checkme(this)"></asp:CheckBox>
                                <%--<input id="ckbRKBH" type="checkbox" runat="server" />--%>
                                <asp:HiddenField ID="hdfRKBH" runat="server" Value='<%#Eval("WG_CODE").ToString() %>'
                                    Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="编号" DataField="WG_CODE" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="供应商名称" DataField="WG_COMPANY" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="日期" DataField="WG_DATE" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="钩稽状态" DataField="WG_GJSTATE" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="制单人" DataField="DocName" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="数量" DataField="Num" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="金额" DataField="Amount" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="入库明细" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlContra" CssClass="hand" ToolTip='<%#Eval("WG_CODE") %>' onClick="javascript:ShowDetail(this);"
                                    runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />查看
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                     <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                    <FixRowColumn FixRowType="Header" TableWidth="99%" TableHeight="380px" />
                 </yyc:SmartGridView>
                <asp:Panel ID="NoDataPanel" runat="server" Visible="false" HorizontalAlign="Center">
                    <hr style="width: 100%; height: 0.1px; color: Blue;" />
                    没有记录！
                </asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>

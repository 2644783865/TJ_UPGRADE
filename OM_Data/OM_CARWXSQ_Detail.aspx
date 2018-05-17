<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CARWXSQ_Detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CARWXSQ_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    用车申请及审批&nbsp;&nbsp;&nbsp; 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript" language="javascript">
function shuru()
{
    var i;
    var table=document.getElementById("tab");
    var tr=table.getElementsByTagName("tr");
    var objstr = '';
    for(i=8;i<tr.length-3;i++)
    {
       var obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];

       if( obj.checked)
       {    
          objstr="checked";
          break;
       }
    }
    if(objstr!="checked")
    {
        alert("请指定要插入行的位置!");
        return false;
    }
}
function checking()
{
    var table=document.getElementById("tab");
    var tr=table.getElementsByTagName("tr");
    var count=0;
   
    for(var i=8;i<tr.length-3;i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        if(obj.checked)
        {    
           count=i;
           break;
        }
    }
    if(count>0)
    {
       var i=confirm('确定删除吗？');
       if(i==true)
       {
            return true;
       }
       else
       {
            for(var i=8;i<tr.length-3;i++)
            {
                obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if(obj.checked)
                {    
                   obj.checked=false;
                }
            }
            return false;
       }
    }
    else            
    {
        alert("请选择要删除的行！");
        return false;
    }
}
function xiaoji(obj)
{
var xiaoji;
var shuliang=$(obj).parent().parent().find("input[name*='count']").val();
var danjia=$(obj).parent().parent().find("input[name*='price']").val();
if(shuliang!=""&&danjia!="")
{
xiaoji=danjia*shuliang;
$(obj).parent().parent().find("input[name*='moneyone']").val(xiaoji);
}
else
{
xiaoji=0;
$(obj).parent().parent().find("input[name*='moneyone']").val(xiaoji);
}
moneyall(obj);
}
function moneyall(obj)
{
 var i;
  var sum=0;
  var table=document.getElementById("tab");
  var tr2= document.getElementById("tr2");
  var tr=table.getElementsByTagName("tr");
  for(i=8;i<tr.length-4;i++)
  {
     var ss=tr[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;
     if(ss=="")
     {
        ss=0;
     }
     sum=parseFloat(ss)+sum;
  }
  var ss=tr.length-4;
  tr[ss].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=sum;

}

    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="update_body" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right ">
                    <div class="box-title">
                        <table style="width: 98%">
                            <tr>
                                <td style="width: 20%" align="right">
                                    操作：
                                </td>
                                <td style="width: 40%" align="left">
                                    <asp:Button ID="btninsert" runat="server" ToolTip="插入选择行下方,不超过10条！！！" Text=" 插 入"
                                        OnClientClick="return shuru()" OnClick="btninsert_Click" BackColor="#99ccff" />
                                    <%--<input id="i" runat="server" type="button" title="插入" style="width:100px;" />--%>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btndelete" runat="server" Text="删 除" OnClientClick="return checking()"
                                        OnClick="btndelete_Click" BackColor="#99ccff" />
                                </td>
                                <td style="width: 20%" align="right">
                                    <asp:Button ID="btnLoad" runat="server" Text="保 存" OnClick="btnLoad_OnClick" BackColor="#99ccff" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_OnClick"
                                        BackColor="#99ccff" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                ActiveTabIndex="0">
                <cc1:TabPanel ID="Tab_sqnr" runat="server" HeaderText="申请内容" TabIndex="0" Width="100%">
                    <ContentTemplate>
                        <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <td style="font-size: large; text-align: center;" colspan="6">
                                            <strong>
                                                <asp:Label ID="iid" runat="server"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            日期
                                        </td>
                                        <td align="center">
                                            <input id="date" runat="server" style="width: 180px" class="easyui-datebox" editable="false"
                                                readonly="readonly" />
                                        </td>
                                        <td align="center">
                                            单号
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtCode" runat="server" Enabled="false" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            车辆名称
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="CARNAME" runat="server" Width="180px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            车辆牌照
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="CARID" runat="server" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="ddlcar_change">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            申请人
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="APPLYER" runat="server" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="lbl_place" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" colspan="2">
                                            <asp:TextBox ID="PLACE" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbl_time" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <input id="PLACEDATE" runat="server" style="width: 180px" class="easyui-datetimebox"
                                                editable="false" onchange="timechange(this)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="lbl_bybefore" runat="server" Text="保养前公里数"></asp:Label>
                                        </td>
                                        <td align="center" colspan="2">
                                            <asp:TextBox ID="txt_before" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbl_byzq" runat="server" Text="保养周期/间隔"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txt_byzq" runat="server" Width="300px"></asp:TextBox>
                                            <%--<input id="Text1" runat="server" style="width: 180px" class="easyui-datetimebox"
                                                editable="false" onchange="timechange(this)" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="lbl_byafter" runat="server" Text="下次保养公里数"></asp:Label>
                                        </td>
                                        <td align="center" colspan="2">
                                            <asp:TextBox ID="txt_after" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbl_byyj" runat="server" Text="保养预警公里数"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txt_byyj" runat="server" Width="300px"></asp:TextBox>
                                            <%--<input id="Text1" runat="server" style="width: 180px" class="easyui-datetimebox"
                                                editable="false" onchange="timechange(this)" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="1">
                                            <asp:Label ID="title" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" colspan="5">
                                            <asp:TextBox ID="txt_contents" runat="server" Width="80%" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                        </td>
                                        <%--<td align="center" colspan="5">
                                            <asp:TextBox ID="TextBox1" runat="server" Width="80%" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            序号
                                        </td>
                                        <td align="center" colspan="1">
                                            名称
                                        </td>
                                        <td align="center" colspan="1">
                                            数量
                                        </td>
                                        <td align="center" colspan="1">
                                            单位
                                        </td>
                                        <td align="center" colspan="1">
                                            单价
                                        </td>
                                        <td align="center" colspan="1">
                                            小计
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="tr2">
                                        <td align="center" style="width: 40px;">
                                            <%#Container.ItemIndex + 1%>&nbsp;<asp:CheckBox ID="cbchecked" CssClass="checkBoxCss"
                                                BorderStyle="None" runat="server"></asp:CheckBox>
                                        </td>
                                        <td align="center" colspan="1">
                                            <%-- <input id="goodsname" runat="server" type="text" value='<%#Eval("GOODSNAME") %>' />--%>
                                            <asp:TextBox ID="goodsname" runat="server" OnTextChanged="Textname_TextChanged" Text='<%#Eval("GOODSNAME")%>'
                                                AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="carkucun"
                                                ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="goodsname"
                                                UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodscount" runat="server" type="text" value='<%#Eval("GOODSCOUNT") %>'
                                                name="count" onkeyup="xiaoji(this)" />
                                        </td>
                                        <td align="center" colspan="1">
                                            <%--<input id="goodsunit" runat="server" type="text" value='<%#Eval("GOODSUNIT") %>' />--%>
                                            <asp:TextBox ID="goodsunit" runat="server" Text='<%#Eval("GOODSUNIT") %>' name="count"
                                                onkeyup="xiaoji(this)"></asp:TextBox>
                                        </td>
                                        <td align="center" colspan="1">
                                            <%--<input id="goodsprice" runat="server" type="text" value='<%#Eval("GOODSPRICE") %>'
                                                name="price" onkeyup="xiaoji(this)" />--%>
                                            <asp:TextBox ID="goodsprice" runat="server" Text='<%#Eval("GOODSPRICE") %>' name="count"
                                                onkeyup="xiaoji(this)"></asp:TextBox>
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="moneyone" runat="server" type="text" value='<%#Eval("MONEYONE") %>' name="xiaoji" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td align="center" colspan="1">
                                        </td>
                                        <td align="center" colspan="1">
                                        </td>
                                        <td align="center" colspan="1">
                                        </td>
                                        <td align="center" colspan="1">
                                        </td>
                                        <td align="center" colspan="1">
                                            合计：
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="moneyall" runat="server" onkeyup="moneyall(this)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="1">
                                            司机班调度意见：
                                        </td>
                                        <td align="center" colspan="5">
                                            <asp:TextBox ID="managerview" runat="server" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="1">
                                            综合管理部负责人：
                                        </td>
                                        <td align="center" colspan="5">
                                            <asp:TextBox ID="controllerview" runat="server" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="1">
                                            维修技师：
                                        </td>
                                        <td align="center" colspan="2">
                                            <asp:TextBox ID="repair" runat="server" Width="80%"></asp:TextBox>
                                        </td>
                                        <td align="center" colspan="1">
                                            申请人：
                                        </td>
                                        <td align="center" colspan="2">
                                            <asp:TextBox ID="apply" runat="server" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tab_spxx" runat="server" HeaderText="审批信息" TabIndex="1" Width="100%">
                    <ContentTemplate>
                        <div class="box-wrapper">
                            <div class="box-outer">
                                <table width="100%">
                                    <tr>
                                        <td style="font-size: large; text-align: center; height: 43px" colspan="2">
                                            申请审批
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="zhaungtai" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="danhao" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="box-outer">
                                <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <tr id="tr1" runat="server">
                                        <td align="center">
                                            调度
                                        </td>
                                        <td colspan="3">
                                            <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                                <tr style="height: 25px">
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_first" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                                                        <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Enabled="false"
                                                            Height="20px">
                                                            <asp:ListItem Text="同意" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="first_opinion" Enabled="false" runat="server" TextMode="MultiLine"
                                                            Width="100%" Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="tr2" runat="server">
                                        <td align="center">
                                            综合办
                                        </td>
                                        <td colspan="3">
                                            <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                                <tr style="height: 25px">
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_second" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                                                        <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" Enabled="false" runat="server"
                                                            Height="20px">
                                                            <asp:ListItem Text="同意" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="second_opinion" runat="server" Enabled="false" TextMode="MultiLine"
                                                            Width="100%" Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="lblState" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

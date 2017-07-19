<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CarApplyDetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarApplyDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    用车申请及审批&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script language="javascript" type="text/javascript">
function timechange(obj)
{
var time=$(obj).val();
console.log(time);
 var time1=document.getElementById('<%=usetime1.ClientID%>').value;
 var time2=document.getElementById('<%=usetime2.ClientID%>').value;
 if(time1=="")
 {alert("请填写正确出发日期！！！");}
 if(time1>time2)
 {
 alert("请填写正确出发/回来日期！！！");
}
    }
    </script>

    <%--<script language="javascript" type="text/javascript">
    //*********************弹出人员子窗口***********************
    var i;
    array = new Array();
//    function SelTechPersons0() 
//    {
//        i = window.showModalDialog('OM_CarPersons.aspx', '', "dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
//        if (i != null) 
//        {
//            array = i.split(' ');
//            document.getElementById('<%=txtshr.ClientID%>').innerText = array[0];
//            document.getElementById('<%=shrid.ClientID%>').value = array[1];
//        }
//        else 
//        {
//            document.getElementById('<%=txtshr.ClientID%>').innerText = "";
//        }
//    }
    function SelTechPersons1() 
    {
        i = window.showModalDialog('OM_CarPersons.aspx', '', "dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
        if (i != null)
        {
            array = i.split(' ');
            document.getElementById('<%=txt_first.ClientID%>').innerText = array[0];
            document.getElementById('<%=firstid.ClientID%>').value = array[1];
        }
        else 
        {
            document.getElementById('<%=txt_first.ClientID%>').innerText = "";
        }
    }
    
    function SelTechPersons2()
    {
        i = window.showModalDialog('OM_CarPersons.aspx', '', "dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
        if(i!=null)
        {
            array=i.split(' ');
            document.getElementById('<%=txt_second.ClientID%>').innerText=array[0];
            document.getElementById('<%=secondid.ClientID%>').value=array[1];
        }
        else
       {
           document.getElementById('<%=txt_second.ClientID%>').innerText="";
       }
    }
</script>--%>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="update_body" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnLoad" runat="server" Text="提 交" Visible="false" OnClick="btnLoad_OnClick"
                                            CssClass="button-outer" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_OnClick"
                                            CausesValidation="False" CssClass="button-outer" />
                                        &nbsp;&nbsp;
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
                            <table id="Table1" align="center" cellpadding="4" cellspacing="1" runat="server"
                                class="toptable grid" border="1">
                                <tr>
                                    <td width="100px">
                                        编号：
                                    </td>
                                    <%--<asp:DropDownList ID="ddlcarnum" runat="server" Width="300px" AutoPostBack="true" />--%>
                                    <td>
                                        <asp:TextBox ID="txtCode" runat="server" Enabled="false" Width="300px"></asp:TextBox>
                                        <span id="span1" runat="server" visible="false" class="Error">*</span>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写编号"
                         ControlToValidate="txtCode" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td width="100px">
                                        发车时间：
                                    </td>
                                    <td>
                                    <input id="ydtime" name="usetime1" runat="server" style="width: 300px" class="easyui-datetimebox"
                                            editable="false" onchange="timechange(this)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        用车部门：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDepartment" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写部门"
                                            ControlToValidate="txtDepartment" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="100px">
                                        车牌号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="carnum" runat="server" Enabled="false" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        外出事由：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReason" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写外出事由"
                                            ControlToValidate="txtReason" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="100px">
                                        司机：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="driver" runat="server" Enabled="false" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        始发地：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_sfd" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请填写始发地"
                                            ControlToValidate="txt_sfd" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="100px">
                                        用车时间：
                                    </td>
                                    <td>
                                        <input id="usetime1" name="usetime1" runat="server" style="width: 300px" class="easyui-datetimebox"
                                            editable="false" onchange="timechange(this)" />
                                    </td>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请填写用车时间"
                                            ControlToValidate="usetime1" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        目的地：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMdd" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写目的地"
                                            ControlToValidate="txtMdd" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="100px">
                                        乘车人数：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNum" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请填写乘车人数"
                                            ControlToValidate="txtNum" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        起始里程：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlicheng1" runat="server" Enabled="false" Width="300px"></asp:TextBox>千米/km
                                    </td>
                                    <td width="100px" visible="false">
                                        实际用车开始时间：
                                    </td>
                                    <td>
                                        <%--<asp:TextBox ID="txtTime1" runat="server" class="easyui-datebox" editable="false" Width="300px"></asp:TextBox>--%>
                                        <input id="txtTime1" runat="server" visible="false" style="width: 300px" readonly="readonly" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        返回里程：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlicheng2" runat="server" Enabled="false" Width="300px"></asp:TextBox>千米/km
                                    </td>
                                    <td width="100px">
                                        结束时间：
                                    </td>
                                    <td>
                                        <%--<asp:TextBox ID="txtTime2" runat="server" class="easyui-datebox" editable="false" Width="300px"></asp:TextBox>--%>
                                        <input id="txtTime2" runat="server" style="width: 300px" readonly="readonly" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        联系电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhone" runat="server" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="请填写联系电话"
                                            ControlToValidate="txtPhone" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td width="100px">
                                        备注：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNote" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        审核类型：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_leixing" runat="server" />
                                        <%--<asp:TextBox ID="txtshr" runat="server" Width="150px"></asp:TextBox>--%>
                                        <%--<input id="shrid" type="text" runat="server" readonly="readonly" style="display:none" />--%>
                                        <%--<asp:HyperLink ID="hlSelect0" runat="server" CssClass="hand" OnClick="SelTechPersons0()">
                        <asp:Image ID="AddImage0" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server"/>
                             选择</asp:HyperLink>--%>
                                        <span id="span2" runat="server" visible="false" class="Error">*</span>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请选择审核人！"
                             ControlToValidate="txtshr" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td width="100px" visible="false">
                                        结束时间：
                                    </td>
                                    <td>
                                        <input id="usetime2" runat="server" visible="false" style="width: 300px" class="easyui-datetimebox"
                                            editable="false" onchange="timechange(this)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        申请人：
                                    </td>
                                    <td>
                                        <asp:Label ID="sqr" runat="server" Width="300px"></asp:Label>
                                    </td>
                                    <td width="100px">
                                        制表时间：
                                    </td>
                                    <td>
                                        <asp:Label ID="sqrq" runat="server" Width="300px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="zhuyi" runat="server">
                                <table id="t" runat="server">
                                    <tr align="center">
                                        <td align="center" style="color: Red; height: 30px; width: 100%">
                                            <strong >注意：申请人因故取消用车时，请及时致电司机班，以免造成其他部门不能正常使用车辆！</strong>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="Tab_spxx" runat="server" HeaderText="审批信息" TabIndex="1" Width="100%">
                        <ContentTemplate>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <table width="100%">
                                        <tr>
                                            <td style="font-size: large; text-align: center; height: 43px" colspan="2">
                                                用车申请审批
                                            </td>
                                            <td>
                                                <asp:Label ID="level" runat="server" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="zhuangtai" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="box-outer">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                        border="1">
                                        <tr id="quxiao" runat="server" visible="false">
                                            <td align="center" style="color: red;">
                                                <strong>取消原因</strong>
                                            </td>
                                            <td style="color: red;">
                                                <strong>
                                                    <asp:Label ID="lblquxiao" runat="server"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server">
                                            <td align="center">
                                                一级审批
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
                                                            <%--<asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                          <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server"/>
                                               选择</asp:HyperLink>--%>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Enabled="false"
                                                                Height="20px">
                                                                <asp:ListItem Text="同意" Value="1" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
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
                                                            <asp:TextBox ID="first_opinion" Enabled="false" runat="server" TextMode="MultiLine"
                                                                Width="100%" Height="42px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="tr2" runat="server">
                                            <td align="center">
                                                二级审批
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
                                                            <%--<asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()"><asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server"/>
                                               选择</asp:HyperLink>--%>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" Enabled="false" runat="server"
                                                                Height="20px">
                                                                <asp:ListItem Text="同意" Value="2" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="5"></asp:ListItem>
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
                                                            <asp:TextBox ID="second_opinion" runat="server" Enabled="false" TextMode="MultiLine"
                                                                Width="100%" Height="42px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="tr3" runat="server">
                                            <td align="center">
                                                三级审批
                                            </td>
                                            <td colspan="3">
                                                <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                                    <tr style="height: 25px">
                                                        <td align="center" style="width: 10%">
                                                            审批人
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txt_third" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                            <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                            <%-- <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()"><asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server"/>
                                               选择</asp:HyperLink>--%>
                                                        </td>
                                                        <td align="center" style="width: 10%">
                                                            审核结论
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            <asp:RadioButtonList ID="rblthird" RepeatColumns="2" Enabled="false" runat="server"
                                                                Height="20px">
                                                                <asp:ListItem Text="同意" Value="4" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Text="不同意" Value="7"></asp:ListItem>
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
                                                            <asp:TextBox ID="third_opinion" runat="server" Enabled="false" TextMode="MultiLine"
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

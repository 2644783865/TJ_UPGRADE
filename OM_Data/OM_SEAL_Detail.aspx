<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="OM_SEAL_Detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Date.OM_SEAL_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
     印章申请及审批&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script language="javascript" type="text/javascript">
    //*********************弹出人员子窗口***********************
    var i;
    array = new Array();
    function SelTechPersons0() 
    {
        i = window.showModalDialog('OM_CarPersons.aspx', '', "dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
        if (i != null) 
        {
            array = i.split(' ');
            document.getElementById('<%=txtshr.ClientID%>').innerText = array[0];
            document.getElementById('<%=shrid.ClientID%>').value = array[1];
        }
        else 
        {
            document.getElementById('<%=txtshr.ClientID%>').innerText = "";
        }
    }
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
</script>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
<asp:UpdatePanel id="update_body" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="RightContent">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnLoad" runat="server" Text="提 交" OnClick="btnLoad_OnClick"  CssClass="button-outer" /> 
                                 &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_OnClick" CausesValidation="False" CssClass="button-outer"/>
                                 &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
         TabStripPlacement="Top" ActiveTabIndex="0">
        <cc1:TabPanel ID="Tab_sqnr" runat="server" HeaderText="申请内容" TabIndex="0" Width="100%">
            <ContentTemplate>
            <table id="Table1" align="center" cellpadding="4" cellspacing="1" runat="server" class="toptable grid" border="1">
                
                <tr>
                    <td width="100px">编号：</td>
                    <td><asp:TextBox ID="txtCode" runat="server" Width="300px"></asp:TextBox>
                    <span id="span1" runat="server" visible="false" class="Error">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写编号"
                         ControlToValidate="txtCode" Display="Dynamic"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <td width="100px">申请部门：</td>
                    <td><asp:TextBox ID="txtDep" runat="server" Width="300px"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td width="100px">盖章人：</td>
                    <td><asp:TextBox ID="txtMan" runat="server" Width="300px"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td width="100px">文件名称：</td>
                    <td><asp:TextBox ID="txtFile" runat="server" Width="300px"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td width="100px">印章类别：</td>
                    <td> 
                        <asp:RadioButtonList ID="rbl_Type" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbl_Type_OnSelectedIndexChanged" AutoPostBack="True">
                             <asp:ListItem Text="行政章" Value = "0" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="合同章" Value = "1"></asp:ListItem>
                             <asp:ListItem Text="公司领导名章" Value = "2"></asp:ListItem>
                             <asp:ListItem Text="其他用章" Value = "3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                
                <tr>
                    <td width="100px">印章数量：</td>
                    <td><asp:TextBox ID="txtNum" runat="server" Width="300px"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td width="100px">备注：</td>
                    <td><asp:TextBox ID="txtNote" runat="server" Width="300px"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td width="100px">审核人：</td>
                    <td>
                        <asp:TextBox ID="txtshr" runat="server" Width="150px"></asp:TextBox>
                        <input id="shrid" type="text" runat="server" readonly="readonly" style="display:none" />
                        <asp:HyperLink ID="hlSelect0" runat="server" CssClass="hand" OnClick="SelTechPersons0()">
                        <asp:Image ID="AddImage0" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server"/>
                             选择</asp:HyperLink>
                        <span id="span2" runat="server" visible="false" class="Error">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请选择审核人！"
                             ControlToValidate="txtshr" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                
                <tr>
                    <td width="100px">申请人：</td>
                    <td><asp:Label ID="sqr" runat="server" Width="300px"></asp:Label></td>
                </tr>
                <tr>
                    <td width="100px">申请日期：</td>
                    <td><asp:Label ID="sqrq" runat="server" Width="300px"></asp:Label></td>
                </tr>
            </table>
            </ContentTemplate>
        </cc1:TabPanel>
        
        <cc1:TabPanel ID="Tab_spxx" runat="server" HeaderText="审批信息" TabIndex="1" Width="100%">
            <ContentTemplate>
            <div class="box-wrapper">
                <div class="box-outer">
                  <table width="100%">
                      <tr>
                          <td style=" font-size:large; text-align:center; height:43px" colspan="2">
                              印章申请审批</td>
                      </tr>
                  </table>
                </div>
                
                <div class="box-outer">
                  <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                      <tr>
                          <td align="center">部门审批</td>
                          <td colspan="3">
                              <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                  <tr style="height: 25px">
                                      <td align="center" style="width:10%">审批人</td>
                                      <td style="width:20%">
                                          <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                          <input id="firstid" type="text" runat="server" readonly="readonly" style="display:none" />
                                          <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                          <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server"/>
                                               选择</asp:HyperLink>
                                      </td>
                                      <td align="center" style="width:10%">审核结论</td>
                                      <td align="center" style="width:20%">
                                          <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Height="20px" >                
                                          <asp:ListItem Text="同意" value="1" ></asp:ListItem>
                                          <asp:ListItem Text="不同意" Value="2" ></asp:ListItem>
                                          </asp:RadioButtonList>
                                      </td>
                                      <td align="center" style="width:10%">审核时间</td>
                                      <td style="width:20%">
                                          <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td colspan="6">
                                          <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%" Height="42px"></asp:TextBox>
                                      </td>
                                  </tr>
                              </table> 
                          </td>
                      </tr>
                      
                      <tr>
                          <td align="center">办公室审批</td>
                          <td colspan="3">
                              <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                  <tr style="height: 25px">
                                      <td align="center" style="width:10%">审批人</td>
                                      <td style="width:20%">
                                          <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                          <input id="secondid" type="text" runat="server" readonly="readonly" style="display:none" />
                                          <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()"><asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server"/>
                                               选择</asp:HyperLink>
                                      </td>
                                      <td align="center" style="width:10%">审核结论</td>
                                      <td align="center" style="width:20%">
                                          <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Height="20px">                
                                          <asp:ListItem Text="同意" value="3" ></asp:ListItem>
                                          <asp:ListItem Text="不同意" Value="4" ></asp:ListItem>
                                          </asp:RadioButtonList>
                                      </td>
                                      <td align="center" style="width:10%">审核时间</td>
                                      <td style="width:20%">
                                          <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td colspan="6">
                                          <asp:TextBox ID="second_opinion" runat="server" TextMode="MultiLine" Width="100%" Height="42px"></asp:TextBox>
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


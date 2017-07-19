<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_Pur_inform_commit_detial.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_Pur_inform_commit_detial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
    $(document).ready(function(){
               $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").hide();
                    $("#<%=Panel4.ClientID%>").hide();
                }
                else if ($(this).val() == "2") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").hide();
                }
                else if ($(this).val() == "3") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").show();
                }
            });

    });
        //**********弹出技术部人员子窗口***********************
        var i;
        array = new Array();
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        function SelTechPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
          function SelTechPersons3() {
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
       
       
        function rblSPJB_onchange() {
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").hide();
                    $("#<%=Panel4.ClientID%>").hide();
                }
                else if ($(this).val() == "2") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").hide();
                }
                else if ($(this).val() == "3") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").show();
                }
            });
        }
        
//         function TestSave(){
//                $("#tdSPLX input:radio:checked").each(function() {
//                    if ($(this).val() == "1") {
//                       if($("#<%=firstid.ClientID %>").val()==""){
//                         alert('请选择审批人后再提交审批！');
//                         return false;
//                         }
//                    }
//                    else if ($(this).val() == "2") {
//                       if($("#<%=firstid.ClientID %>").val()==""||$("#<%=secondid.ClientID %>").val()==""){
//                         alert('请选择审批人后再提交审批！');
//                         return false;
//                         }
//                    }
//                    else if ($(this).val() == "3") {
//                       if($("#<%=firstid.ClientID %>").val()==""||$("#<%=secondid.ClientID %>").val()==""||$("#<%=thirdid.ClientID %>").val()==""){
//                         alert('请选择审批人后再提交审批！');
//                         return false;
//                         }
//                    }
//                });

//              }


    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <asp:HiddenField runat="server" ID="Hidden" />
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td colspan=2 align="right">
                            <asp:Button ID="btnLoad" runat="server" Text="提 交" CssClass="button-outer" OnClick="btnYes_Click"/>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="False" CssClass="button-outer"
                                OnClick="btnReturn_Click" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                    <td colspan=2 style="text-align:center">
                        <strong>采购信息交流单</strong>
                    </td>                   
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            制单人：<asp:Label ID="zdr_pur_commit" runat="server" ></asp:Label>
                        </td>
                        <td>
                            制单时间：<asp:Label ID="zdsj_pur_commit" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0">
        <asp:TabPanel ID="Tab_INFO" runat="server" Width="100%" HeaderText="采购交流信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer">
                        <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                            <tr>
                            <td width="135px">
                                合同号：
                                </td>
                                <td>
                                    <asp:TextBox ID="hth_pur_commit" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                项目名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="xmmc_pur_commit" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                             <tr style="display:none">
                                <td>
                                任务号：
                                </td>
                                <td>
                                    <asp:TextBox ID="rwh_pur_commit" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td width="135px">
                                    信息内容
                                </td>
                                <td>
                                    <asp:TextBox ID="Text_contextmain" runat="server" TextMode="MultiLine" Width="500px"
                                        Height="70px"></asp:TextBox>
                                    <asp:TextBox ID="txtCS_CODE" runat="server" Visible="false" Width="246px"></asp:TextBox>
                                    <asp:TextBox ID="text_spjb" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="text_state" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="text_zdrid" runat="server" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    抄送人员
                                </td>
                                <td>
                                    <asp:Panel ID="Panel2" runat="server" EnableViewState="False">
                                        <asp:Label ID="errorlb" runat="server" EnableViewState="False" ForeColor="Red" Visible="False"></asp:Label>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="Tab_Review" runat="server" Width="100%" HeaderText="评审信息" TabIndex="1">
            <ContentTemplate>
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <tr>
                        <td style="width: 150px">
                            附件：
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Label ID="lblpathName" runat="server" Visible="false"></asp:Label>
                            <asp:Button ID="bntupload" runat="server" Text="上 传" OnClick="bntupload_Click" CausesValidation="False" />
                            <asp:Label ID="filesError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lbreport" runat="server" Visible="False"></asp:Label>
                            <asp:GridView ID="gvfileslist" runat="server" AutoGenerateColumns="False" PageSize="5"
                                CssClass="toptable grid" CellPadding="4" DataKeyNames="ID" ForeColor="#333333"
                                Width="100%">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("ID") %>' ID="lbid"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FILENAME" HeaderText="文件名称" ItemStyle-Width="60%">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UPLOADDATE" HeaderText="上传时间">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="删除">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                                OnClick="imgbtndelete_Click" CausesValidation="False" ToolTip="删除" Width="15px"
                                                Height="15px" />
                                        </ItemTemplate>
                                        <ControlStyle Font-Size="Small" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="下载">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtndownload" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                                OnClick="imgbtndownload_Click" CausesValidation="False" ToolTip="下载" Width="15px"
                                                Height="15px" />
                                        </ItemTemplate>
                                        <ControlStyle Font-Size="Small" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Font-Size="Small"
                                    Height="10px" />
                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="pc_pur_inform_pro" runat="server" Width="100%" HeaderText="评审信息"
            TabIndex="2">
            <ContentTemplate>
                <div class="box-outer">
                    <asp:Panel runat="server" ID="panel_pro">
                        <table width="100%">
                            <tr style="height: 25px">
                                <td style="text-align: right">
                                    审批类型：
                                </td>
                                <td id="tdSPLX" colspan="3">
                                    <asp:RadioButtonList runat="server" ID="rblSPJB" RepeatDirection="Horizontal" RepeatColumns="3"
                                        Width="320px" onchange="rblSPJB_onchange()" Style="margin: auto">
                                        <asp:ListItem Text="一级审批" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审批" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="三级审批" Value="3" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Panel1">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center" style="width: 70px;">
                                    采购部审核</br></br><p style="color: Red">
                                        <asp:Label ID="lbtips1" runat="server" Text="请选择采购部部长"></asp:Label></p>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
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
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Panel3">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center" style="width: 70px;">
                                    技术部审核</br></br><p style="color: Red">
                                        <asp:Label ID="lbtips2" runat="server" Text="请选择技术部人员"></asp:Label>
                                    </p>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()">
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
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Panel4">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center" style="width: 70px;">
                                    技术部审核</br></br><p style="color: Red">
                                        <asp:Label ID="lbtips3" runat="server" Text="请选择技术部部长"></asp:Label></p>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelTechPersons3()">
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
                        </table>
                    </asp:Panel>
                </div>
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

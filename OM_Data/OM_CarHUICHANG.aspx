<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CarHUICHANG.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarHUICHANG" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车辆回厂&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        function fache_confirm(){
        var fa_hui_charge='<%=action%>';   
            if(fa_hui_charge=="huiche"){
                var fa_hui_kai=parseInt('<%=get_fache_mile() %>');
                var fa_hui_jie=parseInt(document.getElementById("<%=jslc.ClientID%>").value);  
                if(fa_hui_jie){
                      if((fa_hui_jie-fa_hui_kai)>500){
                              return confirm('结束里程数和开始里程数之差大于500，可能是输入错误，是否继续输入？')
                      }
                 }
            }
            if(fa_hui_charge=="fache"){
                var fache_time=document.getElementById("<%=ydtime.ClientID%>").value;
                var sj_dropd=document.getElementById("<%=sj.ClientID%>");
                var sj_dropd_index=sj_dropd.selectedIndex;
                var sj_tel=document.getElementById("<%=sjcall.ClientID%>").value;
                if(fache_time==""){
                    alert('所有信息必须填写完整');
                    return false;
                 }
                 if(sj_dropd_index=="0"){
                    alert('请选择司机');
                    return false;
                 }
                 if(sj_tel==""){
                    alert('请填写司机联系电话');
                    return false;
                 }
                 if(!/^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$/.test(fache_time)){
                    alert("请输入正确的日期时间格式,如2016-04-09 11:08:33");
                    return false;
                }
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-wrapper">
                <table style="width: 98%;">
                    <tr align="center">
                        <td>
                            <asp:TextBox ID="lbllc1" Visible="false" Width="200px" runat="server" />
                        </td>
                    </tr>
                    <tr id="TR1" runat="server" align="left">
                        <td id="TD1" style="height: 20px" align="center">
                            结束里程数:<asp:TextBox ID="jslc" runat="server" Width="200px"></asp:TextBox>千米/km <span
                                id="span1" runat="server" visible="false" class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写里程数"
                                ControlToValidate="jslc" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%--  <tr id="TR6" style="height: 20px" runat="server">
                        <td align="center">
                            用车开始时间:<input id="kstime" runat="server" style="width: 200px" class="easyui-datetimebox"
                                editable="false" /><span id="span6" runat="server" visible="false" class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="请填写用车开始时间"
                                ControlToValidate="kstime" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>--%>
                    <tr id="TR5" style="height: 20px" runat="server" align="left">
                        <td align="center">
                            用车结束时间:<input id="jstime" runat="server" style="width: 200px" class="easyui-datetimebox"
                                editable="false" /><span id="span5" runat="server" visible="false" class="Error">*</span>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请填写用车结束时间"
                                ControlToValidate="jstime" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr id="TR8" style="height: 20px" runat="server" align="left">
                        <td align="center">
                            备注:<asp:TextBox ID="note" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="TR4" style="height: 20px" runat="server" align="left">
                        <td align="center">
                            预定发车时间:<input id="ydtime" runat="server" style="width: 200px" class="easyui-datetimebox"
                                editable="false" /><span id="span4" runat="server" visible="false" class="Error">*</span>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请填写发车时间"
                                ControlToValidate="ydtime" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr id="TR2" style="height: 20px" runat="server" align="left">
                        <td align="center">
                            司机姓名:
                            <asp:DropDownList ID="sj" runat="server" Width="200px" OnSelectedIndexChanged="sjchanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <span id="span2" runat="server" visible="false" class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请填写司机姓名"
                                ControlToValidate="sj" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="TR3" style="height: 20px" runat="server" align="left">
                        <td align="center">
                            联系方式:<asp:TextBox ID="sjcall" runat="server" Width="200px"></asp:TextBox><span id="span3"
                                runat="server" visible="false" class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请填写联系方式"
                                ControlToValidate="sjcall" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="TR7" style="height: 20px" runat="server" align="left">
                        <td align="center">
                            取消原因:<asp:TextBox ID="cancel" runat="server" Width="200px" Height="60px" TextMode="MultiLine"></asp:TextBox><span
                                id="span7" runat="server" visible="false" class="Error">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="请填写取消原因"
                                ControlToValidate="cancel" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="OK" runat="server" OnClick="OK_CLICK" Text="确定" Width="100px" OnClientClick="javascript:return fache_confirm()" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

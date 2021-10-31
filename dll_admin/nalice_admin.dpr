library nalice_admin;

{ Important note about DLL memory management: ShareMem must be the
  first unit in your library's USES clause AND your project's (select
  Project-View Source) USES clause if your DLL exports any procedures or
  functions that pass strings as parameters or function results. This
  applies to all strings passed to and from your DLL--even those that
  are nested in records and classes. ShareMem is the interface unit to
  the BORLNDMM.DLL shared memory manager, which must be deployed along
  with your DLL. To avoid using BORLNDMM.DLL, pass string information
  using PChar or ShortString parameters. }

uses
  frmU in 'frmU.pas' {frm},
  System,
  Dialogs,
  SysUtils,
  Windows;

{$R *.res}

function mysql_thread_id(): integer; stdcall; external 'libmysql.dll';

// manager pass = iof76jmo8hf235yfh2nohgo
// reader pass  = 75nfhowyJHTHgHnV5vfd

function GetResUSDJPY(): integer; stdcall;
var
  frm : Tfrm;
begin
  frm := Tfrm.Create(nil);

  try

  if not frm.connect1.Connected then
    frm.connect1.Connect;

  frm.qUSDJPY.Open;

  if frm.qUSDJPY.RecordCount > 0 then
  begin
    if pos('U', frm.qUSDJPY.Fields[0].asstring) > 0 then
      Result := 1
    else
    if pos('D', frm.qUSDJPY.Fields[0].asstring) > 0 then
      Result := 2
    else
      Result := 0;
  end
  else
    Result := -1;
  except
    on e:exception do
    begin
      showmessage(e.Message);
      result := -1;
    end;
  end;

  frm.Free;
end;

function GetResGBPUSD(): integer; stdcall;
var
  frm : Tfrm;
begin
  frm := Tfrm.Create(nil);

  try

  if not frm.connect1.Connected then
    frm.connect1.Connect;

  frm.qGBPUSD.Open;

  if frm.qGBPUSD.RecordCount > 0 then
  begin
    if pos('U', frm.qGBPUSD.Fields[0].asstring) > 0 then
      Result := 1
    else
    if pos('D', frm.qGBPUSD.Fields[0].asstring) > 0 then
      Result := 2
    else
      Result := 0;
  end
  else
    Result := -1;
  except
    on e:exception do
    begin
      showmessage(e.Message);
      result := -1;
    end;
  end;

  frm.Free;
end;

function ClearGBPUSD(): integer; stdcall;
var
  frm : Tfrm;
begin
  frm := Tfrm.Create(nil);

  try

  if not frm.connect1.Connected then
    frm.connect1.Connect;

  frm.qClearGBPUSD.Open;

  Result := 1;
  except
    on e:exception do
    begin
      showmessage(e.Message);
      result := -1;
    end;
  end;

  frm.Free;
end;

function ClearUSDJPY(): integer; stdcall;
var
  frm : Tfrm;
begin
  frm := Tfrm.Create(nil);

  try

  if not frm.connect1.Connected then
    frm.connect1.Connect;

  frm.qClearUSDJPY.Open;

  Result := 1;
  except
    on e:exception do
    begin
      showmessage(e.Message);
      result := -1;
    end;
  end;

  frm.Free;
end;

exports GetResUSDJPY, GetResGBPUSD, ClearGBPUSD, ClearUSDJPY;

begin
end.
 
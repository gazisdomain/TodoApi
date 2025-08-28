{{- define "todoapi.labels" -}}
app.kubernetes.io/name: {{ include "todoapi.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{- define "todoapi.name" -}}
{{ .Chart.Name }}
{{- end }}
